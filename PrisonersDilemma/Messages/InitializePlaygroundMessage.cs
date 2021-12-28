using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class InitializePlaygroundMessage
    {
        public Type Player1 { get; set; }
        public Type Player2 { get; set; }

        public IList<RoundResultMessage> Data { get; set; }
    }
}
