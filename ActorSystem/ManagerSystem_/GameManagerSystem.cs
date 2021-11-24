using ActorSystems.Messages_;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorSystems.ManagerSystem_
{
    public class GameManagerSystem : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if(message is StartGamesMessage)
            {
                var startGameMsg = (StartGamesMessage)message;

                for (int i = 0; i < startGameMsg.GameInfo.NumberOfGames; i++)
                {
                    var gameManaer = Context.ActorOf<GameManager>($"{nameof(GameManager)}_{Guid.NewGuid()}");
                    gameManaer.Tell(new StartGameMessage(startGameMsg.PlaygroundSystem,
                        startGameMsg.PlayerSystem,startGameMsg.DataSystem,i,startGameMsg.GameInfo.NumberOfRounds));
                }
            }
            else
                throw new NotImplementedException();
        }
    }
}
