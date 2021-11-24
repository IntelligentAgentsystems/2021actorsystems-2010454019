using ActorSystems.Messages_;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorSystems.PlaygroundSystem_
{
    public class PlaygroundSystem : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if(message is GetPlaygroundMessage)
            {
                Sender.Tell(new PlaygroundMessage(Context.ActorOf<Playground>($"{nameof(Playground)}_{Guid.NewGuid()}")));
            }
            else
                throw new NotImplementedException();
        }
    }
}
