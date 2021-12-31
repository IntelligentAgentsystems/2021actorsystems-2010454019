using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class InitializeReaderMessage
    {
        public string IdGame { get; private set; }

        public InitializeReaderMessage(string idGame)
        {
            IdGame = idGame;
        }
    }
}
