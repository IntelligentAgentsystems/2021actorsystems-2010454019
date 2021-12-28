using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class InitializeGamesMessage
    {
        public int Rounds { get; set; }
        public int Games { get; set; }
    }
}
