using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class StartRoundMessage
    {
        public static StartRoundMessage Instance => new StartRoundMessage();
    }
}
