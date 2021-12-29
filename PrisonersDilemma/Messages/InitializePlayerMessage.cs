using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class InitializePlayerMessage
    {
        public int PlayerNr { get; set; }
        public IList<ResultMessage> Data { get; set; }
    }
}
