using Akka.Actor;
using Newtonsoft.Json;
using PrisonersDilemma.Helper;
using PrisonersDilemma.Messages;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma
{
    internal class BrokerCommunicator : ReceiveActor
    {

        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        private IBasicProperties props;
        public BrokerCommunicator()
        {
            ReceiveAsync<InitializeWriterMessage>(OnReceive_InitializeWriterMessage);
            ReceiveAsync<ResultMessage>(OnReceive_ResultMessage);
            ReceiveAsync<GetDataMessage>(OnReceive_GetDataMessage);
        }

        protected override void PostStop()
        { 
            channel?.Close();
            connection?.Close();
            base.PostStop();
        }

        private async Task OnReceive_InitializeWriterMessage(InitializeWriterMessage message)
        {
            Sender.Tell(await Try<InitializeFinishedMessage>.Of(async () =>
            {
                factory = new ConnectionFactory() { HostName = message.Hostname, Port=message.Port };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();

                props = channel.CreateBasicProperties();
                props.DeliveryMode = 2;
                props.ContentType = "text/plain";

                return InitializeFinishedMessage.Instance;
            }));          
        }
        private async Task OnReceive_ResultMessage(ResultMessage message)
        {
            Sender.Tell(await Try<FinishedMessage>.Of(async () =>
            {
                Utils.MayFail();
                
                channel.BasicPublish(exchange: Utils.ExchangeName,
                    routingKey: message.IdGame,
                    basicProperties: props,
                    body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
               
                return FinishedMessage.Instance;
            }));
        }

        private async Task OnReceive_GetDataMessage(GetDataMessage message)
        {
            Sender.Tell(await Try<GameDataMessage>.Of(async () =>
            {
                Utils.MayFail();

                var data = new List<ResultMessage>();

                var result = channel.BasicGet(queue: message.IdGame, autoAck: false);
                while (result != null)
                {
                    data.Add(JsonConvert.DeserializeObject<ResultMessage>(Encoding.UTF8.GetString(result.Body.ToArray())));
                    result = channel.BasicGet(queue: message.IdGame, autoAck: false);
                }


                return new GameDataMessage(data: data);
            }));
        }
    }
}
