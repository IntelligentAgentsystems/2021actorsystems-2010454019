using Akka.Actor;
using Newtonsoft.Json;
using PrisonersDilemma.Helper;
using PrisonersDilemma.Messages;
using PrisonersDilemma.Players;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma
{
    internal class GameManager : ReceiveActor
    {

        public GameManager()
        {
            ReceiveAsync<GameStartMessage>(OnReceive_GameStartMessage);         
        }

        private async Task OnReceive_GameStartMessage(GameStartMessage message)
        {
            Sender.Tell(await Try<GameFinishMessage>.Of(async () =>
            {
                Utils.MayFail();

                Console.WriteLine($"{message.Properties.IdGame}->AquireResources");
                var writer = Context.ActorOf<BrokerCommunicator>($"{nameof(BrokerCommunicator)}_{Guid.NewGuid()}");

                (await writer.Ask<Try<InitializeFinishedMessage>>(new InitializeWriterMessage(message.Properties.IdGame, message.Hostname, message.Port))).OrElseThrow();

                Console.WriteLine($"{message.Properties.IdGame}->Started");

                var playground = Context.ActorOf<Playground>($"{nameof(Playground)}_{Guid.NewGuid()}");
               
                var data = (await writer.Ask<Try<GameDataMessage>>(new GetDataMessage(message.Properties.IdGame))).OrElseThrow();
                


                (await playground.Ask<Try<InitializeFinishedMessage>>(
                    new InitializePlaygroundMessage(player1:message.Properties.Player1,player2:message.Properties.Player2,data:data?.Data)
                    )).OrElseThrow();


                int i = data == null || data.Data == null ||  data.Data.Count() == 0 ? 0 : data.Data.Max(e => e.Round) + 1;                
                while (i < message.Properties.Rounds)
                {
                    var result = (await playground.Ask<Try<RoundResultMessage>>(StartRoundMessage.Instance)).OrElseThrow();
                    (await writer.Ask<Try<FinishedMessage>>(new ResultMessage(idGame:message.Properties.IdGame,round:i,player1Result:result.Player1Result,
                        player1Tip:result.Player1Tip,player2Result:result.Player2Result,player2Tip:result.Player2Tip))).OrElseThrow();
                  

                    Console.WriteLine($"{message.Properties.IdGame}->Round:{i}-Finished");

                    i++;
                }
                return new GameFinishMessage(message.Properties.IdGame);
            }));
        }
    }
}
