using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class InitializePlayerMessage
    {
        public InitializePlayerMessage(int playerNr, IList<ResultMessage> data)
        {
            PlayerNr = playerNr;
            Data = data;
        }

        public int PlayerNr { get; private set; }
        public IList<ResultMessage> Data { get; private set; }
    }
}
