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
            Receive<InitializeGamesMessage>(OnReceive_InitializeGamesMessage);
        }

        private void OnReceive_InitializeGamesMessage(InitializeGamesMessage message)
        {
            for (int i = 0; i < message.Games; i++)
            {
                var gameManaer = Context.ActorOf<GameManager>($"{nameof(GameManager)}_{Guid.NewGuid()}");
                gameManaer.Tell(new GameStartMessage() { IdGame = Guid.NewGuid(), Rounds = message.Rounds,
                Player1 = typeof(RandomPlayer), Player2= typeof(RandomPlayer)});
            }
           
        }
    }

 
}
