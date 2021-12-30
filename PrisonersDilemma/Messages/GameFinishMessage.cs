using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class GameFinishMessage
    {
        public Guid IdGame { get; private set; }

        public GameFinishMessage(Guid idGame)
        {
            IdGame = idGame;
        }
    }
}
