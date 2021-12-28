using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class RoundResultMessage
    {
        public int Round { get; set; }
        public bool Player1Tip { get; set; }
        public bool Player2Tip { get; set; }

        public int Player1Result { get; set; }
        public int Player2Result { get; set; }
    }
}
