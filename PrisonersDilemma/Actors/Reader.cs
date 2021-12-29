using Akka.Actor;
using PrisonersDilemma.Helper;
using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma
{
    internal class Reader : ReceiveActor
    {

        public Reader()
        {
            ReceiveAsync<GetDataMessage>(OnReceive_GetDataMessage);
        }


        private async Task OnReceive_GetDataMessage(GetDataMessage message)
        {
            var data = DummyStorage.Instance.Data.Where(e => e.IdGame == message.IdGame).ToList();
            Sender.Tell(new GameDataMessage() { Data = data });
        }
    }
}
