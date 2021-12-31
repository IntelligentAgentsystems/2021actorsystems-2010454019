using Akka.Actor;
using PrisonersDilemma.Helper;
using PrisonersDilemma.Messages;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Actors
{
    internal class BrokerManager : ReceiveActor
    {
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;

        public BrokerManager()
        {
            ReceiveAsync<SetupQueueMessage>(OnReceive_SetupQueueMessage);
            ReceiveAsync<DeleteQueueMessage>(OnReceive_DeleteQueueMessage);
        }

        private async Task OnReceive_SetupQueueMessage(SetupQueueMessage message)
        {
            Sender.Tell(await Try<FinishedMessage>.Of(async () =>
            {
                if (factory == null)
                {
                    factory = new ConnectionFactory() { HostName = "localhost" };
                    connection = factory.CreateConnection();
                    

                    try
                    {
                        channel = connection.CreateModel();
                        channel.ExchangeDeclarePassive(exchange: Utils.ExchangeName);
                    }
                    catch (Exception)
                    {
                        channel = connection.CreateModel();
                        channel.ExchangeDeclare(exchange: Utils.ExchangeName,
                                            type: "direct",durable:true,autoDelete:false);
                    }
                }

                foreach (var item in message.GameIds)
                {
                   
                    try
                    {
                        channel.QueueDeclarePassive(queue: item);
                    }
                    catch (Exception)
                    {
                        channel = connection.CreateModel();
                        channel.QueueDeclare(queue: item, durable: true, autoDelete: false, exclusive: false);
                        channel.QueueBind(queue: item,
                                                exchange: Utils.ExchangeName,
                                                routingKey: item);
                    }
                }

                return FinishedMessage.Instance;
            }));                 
        }

        private async Task OnReceive_DeleteQueueMessage(DeleteQueueMessage message)
        {
            Sender.Tell(await Try<FinishedMessage>.Of(async () =>
            {
                foreach (var item in message.GameIds)
                {
                    channel.QueueDelete(queue: item);
                }

                channel.ExchangeDelete(Utils.ExchangeName);

                channel.Close();
                connection.Close();
                factory = null;

                return FinishedMessage.Instance;
            }));
        }
    }
}
