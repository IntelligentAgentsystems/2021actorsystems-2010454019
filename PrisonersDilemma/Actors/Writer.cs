using Akka.Actor;
using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma
{
    internal class Writer : ReceiveActor
    {

        public Writer()
        {
            ReceiveAsync<ResultMessage>(async e => await Task.Run(() => OnReceive_ResultMessage(e)));
        }

        private void OnReceive_ResultMessage(ResultMessage message)
        {
            Console.WriteLine($"{message.IdGame}-{message.Round}-{message.Player1Result}-{message.Player2Result}");
        }
    }
}
