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
    internal class Writer : ReceiveActor
    {

        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        public Writer()
        {
            ReceiveAsync<InitializeWriterMessage>(OnReceive_InitializeWriterMessage);
            ReceiveAsync<ResultMessage>(OnReceive_ResultMessage);
        }

        protected override void PostStop()
        {
            base.PostStop();
            channel?.Close();
            connection?.Close();
        }

        private async Task OnReceive_InitializeWriterMessage(InitializeWriterMessage message)
        {
            Sender.Tell(await Try<InitializeFinishedMessage>.Of(async () =>
            {
                factory = new ConnectionFactory() { HostName = "localhost" };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();

                return InitializeFinishedMessage.Instance;
            }));          
        }
        private async Task OnReceive_ResultMessage(ResultMessage message)
        {
            Sender.Tell(await Try<FinishedMessage>.Of(async () =>
            {
                Utils.MayFail();

                //DummyStorage.Instance.AddData(message);
                
                channel.BasicPublish(exchange: Utils.ExchangeName,
                    routingKey: message.IdGame,
                    basicProperties: null,
                    body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));

                Console.WriteLine($"{message.IdGame}-{message.Round}-{message.Player1Result}-{message.Player2Result}");
                return FinishedMessage.Instance;
            }));
        }
    }
}
