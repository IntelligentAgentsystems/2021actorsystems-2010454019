using ActorSystems.Messages_;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorSystems.DataSystem_
{
    public class DataSystem : UntypedActor
    {
        private IList<GameMessage> DataStorage = new List<GameMessage>();

        private IActorRef Writer;
        protected override void OnReceive(object message)
        {
            if(message is InitMessage)
            {
                Writer = Context.ActorOf(Props.Create(() => new Writer(ref DataStorage)), "Writer");
            }
            else if(message is GetWriterMessage)
            {
                Sender.Tell(new WriterMessage(Writer));
            }
            else if(message is GetReaderMessage)
            {
                Sender.Tell(new ReaderMessage(Context.ActorOf(Props.Create(() => new Reader(ref DataStorage)), $"{nameof(Reader)}_{Guid.NewGuid()}")));
            }
            else
                throw new NotImplementedException();
        }
    }
}
