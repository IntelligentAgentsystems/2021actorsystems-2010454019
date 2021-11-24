using ActorSystems.Messages_;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorSystems.DataSystem_
{
    public class Writer : UntypedActor
    {
        private IList<GameMessage> storage;
        public Writer(ref IList<GameMessage> storage)
        {
            this.storage = storage;
        }
        
        protected override void OnReceive(object message)
        {
            if(message is GameMessage)
            {
                var gamesMessage = (GameMessage)message;
                Console.WriteLine($"{gamesMessage.GameId}-{gamesMessage.Round}-{gamesMessage.Player1Tip}-{gamesMessage.Player2Tip}");
                storage.Add((GameMessage)message);
            }
            else
                throw new NotImplementedException();
        }
    }
}
