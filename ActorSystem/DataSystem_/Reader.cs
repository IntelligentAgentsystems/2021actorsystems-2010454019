using ActorSystems.Messages_;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorSystems.DataSystem_
{
    public class Reader : UntypedActor
    {
        private IList<GameMessage> storage;
        public Reader(ref IList<GameMessage> storage)
        {
            this.storage = storage;
        }
        protected override void OnReceive(object message)
        {
            if(message is GetGameDataMessage)
            {
                Sender.Tell(new GameDataMessage(storage.Where(e => e.GameId == ((GetGameDataMessage)message).GameId).ToList()));
            }
            else
                throw new NotImplementedException();
        }
    }
}
