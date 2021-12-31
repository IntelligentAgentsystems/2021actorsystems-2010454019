using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class GameFinishMessage
    {
        public string IdGame { get; private set; }

        public GameFinishMessage(string idGame)
        {
            IdGame = idGame;
        }
    }
}
