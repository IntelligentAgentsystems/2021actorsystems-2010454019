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
    internal class GameOperator : ReceiveActor
    {
        public GameOperator()
        {
            ReceiveAsync<InitializeGamesMessage>(OnReceive_InitializeGamesMessage);
        }

        private async Task OnReceive_InitializeGamesMessage(InitializeGamesMessage message)
        {
            var games = new Task[message.Properties.Count()];
            for (int i = 0; i < message.Properties.Count(); i++)
            {
                var gameManaer = Context.ActorOf<GameManager>($"{nameof(GameManager)}_{Guid.NewGuid()}");

                var props = message.Properties[i];
                games[i] = gameManaer.Ask<FinishedMessage>(new GameStartMessage()
                {
                   Properties =props
                }) ;
            }

            await Task.WhenAll(games);
            Sender.Tell(new FinishedMessage());
        }
    }
}
