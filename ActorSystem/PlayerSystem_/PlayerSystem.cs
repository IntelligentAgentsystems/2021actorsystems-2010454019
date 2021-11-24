using ActorSystems.Messages_;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorSystems.PlayerSystem_
{
    public class PlayerSystem : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if(message is GetPlayersMessage)
            {
                var pMsg = (GetPlayersMessage)message;

                var players = new IActorRef[pMsg.Count];

                for (int i = 0; i < pMsg.Count; ++i)
                {
                    players[i] = Context.ActorOf<RandomPlayer>($"{nameof(RandomPlayer)}_{Guid.NewGuid()}");
                }
                Sender.Tell(new PlayersMessage(players));
            }
            else
                throw new NotImplementedException();
        }
    }
}
