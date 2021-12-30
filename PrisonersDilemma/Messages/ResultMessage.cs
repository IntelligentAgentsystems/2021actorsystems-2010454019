using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class ResultMessage
    {
        public ResultMessage(Guid idGame, int round, bool player1Tip, bool player2Tip, int player1Result, int player2Result)
        {
            IdGame = idGame;
            Round = round;
            Player1Tip = player1Tip;
            Player2Tip = player2Tip;
            Player1Result = player1Result;
            Player2Result = player2Result;
        }

        public Guid IdGame { get; private set; }
        public int Round { get; private set; }
        public bool Player1Tip { get; private set; }
        public bool Player2Tip { get; private set; }

        public int Player1Result { get; private set; }
        public int Player2Result { get; private set; }
    }
}
