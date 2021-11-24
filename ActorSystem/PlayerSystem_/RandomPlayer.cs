using ActorSystems.Messages_;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorSystems.PlayerSystem_
{
    class RandomPlayer : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if(message is GetTipMessage)
            {
                var getTipMessage = (GetTipMessage)message;
                //Do intelligent with gamedata
                Sender.Tell(new TipMessage(new System.Random().Next(0, 2) == 0));
            }
            else
                throw new NotImplementedException();
        }
    }
}
