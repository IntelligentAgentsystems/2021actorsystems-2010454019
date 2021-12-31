using Akka.Actor;
using Newtonsoft.Json;
using PrisonersDilemma.Helper;
using PrisonersDilemma.Messages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma
{
    internal class Reader : ReceiveActor
    {
        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;


        public Reader()
        {
            ReceiveAsync<InitializeReaderMessage>(OnReceive_InitializeReaderMessage);
            ReceiveAsync<GetDataMessage>(OnReceive_GetDataMessage);
        }
        protected override void PostStop()
        {
            base.PostStop();
            channel?.Close();
            connection?.Close();

        }

        private async Task OnReceive_InitializeReaderMessage(InitializeReaderMessage message)
        {
            Sender.Tell(await Try<InitializeFinishedMessage>.Of(async () =>
            {
                factory = new ConnectionFactory() { HostName = "localhost" };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();

                
                return InitializeFinishedMessage.Instance;
            }));
        }

        private async Task OnReceive_GetDataMessage(GetDataMessage message)
        {
            Sender.Tell(await Try<GameDataMessage>.Of(async () =>
            {
                Utils.MayFail();

                var data = new List<ResultMessage>();

                var result = channel.BasicGet(queue: message.IdGame, autoAck: false);
                while(result != null)
                {
                    data.Add(JsonConvert.DeserializeObject<ResultMessage>(Encoding.UTF8.GetString(result.Body.ToArray())));
                    result = channel.BasicGet(queue: message.IdGame, autoAck: false);                
                }


                return new GameDataMessage(data: data);
            }));
        }
    }
}
