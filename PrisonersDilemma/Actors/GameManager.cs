using Akka.Actor;
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
            var playground = Context.ActorOf<Playground>($"{nameof(Playground)}_{Guid.NewGuid()}");
            var writer = Context.ActorOf<Writer>($"{nameof(Writer)}_{Guid.NewGuid()}");
            var reader = Context.ActorOf<Reader>($"{nameof(Reader)}_{Guid.NewGuid()}");

            var data = await reader.Ask<GameDataMessage>(new GetDataMessage() { IdGame = message.Properties.IdGame });

            await playground.Ask<InitializeFinishedMessage>(new InitializePlaygroundMessage()
            {
                Player1 = message.Properties.Player1,
                Player2 = message.Properties.Player2,
                Data = data?.Data
            }, TimeSpan.FromSeconds(5));


            int i = data == null || data.Data.Count() == 0 ? 0 : data.Data.Max(e=>e.Round)+1;
            while (i < message.Properties.Rounds)
            {
                var result = await playground.Ask<RoundResultMessage>(new StartRoundMessage());
                await writer.Ask(new ResultMessage()
                {
                    IdGame = message.Properties.IdGame,
                    Round = i,
                    Player1Result = result.Player1Result,
                    Player1Tip = result.Player1Tip,
                    Player2Result = result.Player2Result,
                    Player2Tip = result.Player2Tip
                });
                i++;
            }
            Sender.Tell(new FinishedMessage());
        }
    }
}
