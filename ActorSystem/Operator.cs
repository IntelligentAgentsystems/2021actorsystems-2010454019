using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActorSystems.PlaygroundSystem_;
using ActorSystems.ManagerSystem_;
using ActorSystems.PlayerSystem_;
using ActorSystems.DataSystem_;
using ActorSystems.Messages_;

namespace ActorSystems
{
    public class Operator : UntypedActor
    {
        private IActorRef playgroundSystem;
        private IActorRef gameManagerSystem;
        private IActorRef playerSystem;
        private IActorRef dataSystem;
        protected override void OnReceive(object message)
        {
            if(message is InitMessage)
            {
                playgroundSystem = Context.ActorOf<PlaygroundSystem>(nameof(PlaygroundSystem));
                gameManagerSystem = Context.ActorOf<GameManagerSystem>(nameof(GameManagerSystem));
                playerSystem = Context.ActorOf<PlayerSystem>(nameof(PlayerSystem));
                dataSystem = Context.ActorOf<DataSystem>(nameof(DataSystem));

                dataSystem.Tell(message);

            }
            else if(message is InitializeGamesMessage)
            {
                gameManagerSystem.Tell(new StartGamesMessage(playgroundSystem,playerSystem,dataSystem, (InitializeGamesMessage)message));
            }
            else
            {
                throw new NotImplementedException();
            }           
        }
    }
}
