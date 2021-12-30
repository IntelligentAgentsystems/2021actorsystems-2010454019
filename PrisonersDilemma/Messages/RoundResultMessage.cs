using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class RoundResultMessage
    {
        public RoundResultMessage(bool player1Tip, bool player2Tip, int player1Result, int player2Result)
        {
            Player1Tip = player1Tip;
            Player2Tip = player2Tip;
            Player1Result = player1Result;
            Player2Result = player2Result;
        }

        public bool Player1Tip { get; private set; }
        public bool Player2Tip { get; private set; }

        public int Player1Result { get; private set; }
        public int Player2Result { get; private set; }
    }
}
