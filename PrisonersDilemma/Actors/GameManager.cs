using Akka.Actor;
using PrisonersDilemma.Helper;
using PrisonersDilemma.Messages;
using PrisonersDilemma.Players;
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

                var playground = Context.ActorOf<Playground>($"{nameof(Playground)}_{Guid.NewGuid()}");
                var writer = Context.ActorOf<Writer>($"{nameof(Writer)}_{Guid.NewGuid()}");
                var reader = Context.ActorOf<Reader>($"{nameof(Reader)}_{Guid.NewGuid()}");

                var data = (await reader.Ask<Try<GameDataMessage>>(new GetDataMessage(message.Properties.IdGame),Utils.Timeout_Reader_GetData)).OrElseThrow();

                (await playground.Ask<Try<InitializeFinishedMessage>>(
                    new InitializePlaygroundMessage(player1:message.Properties.Player1,player2:message.Properties.Player2,data:data?.Data),
                    Utils.Timeout_Playground_Initialize))
                    .OrElseThrow();


                int i = data == null || data.Data.Count() == 0 ? 0 : data.Data.Max(e => e.Round) + 1;
                Console.WriteLine($"{message.Properties.IdGame}-Starting at round {i}");
                while (i < message.Properties.Rounds)
                {
                    var result = (await playground.Ask<Try<RoundResultMessage>>(StartRoundMessage.Instance,Utils.Timeout_Playground_StartRound)).OrElseThrow();
                    (await writer.Ask<Try<FinishedMessage>>(new ResultMessage(idGame:message.Properties.IdGame,round:i,player1Result:result.Player1Result,
                        player1Tip:result.Player1Tip,player2Result:result.Player2Result,player2Tip:result.Player2Tip),Utils.Timeout_Writer_Result)).OrElseThrow();
                    i++;
                }
                return new GameFinishMessage(message.Properties.IdGame);
            }));
        }
    }
}
