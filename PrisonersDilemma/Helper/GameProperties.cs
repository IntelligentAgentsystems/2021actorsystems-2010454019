using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Helper
{

    internal class GameProperties
    {
        public string IdGame { get; set; }
        public int Rounds { get; set; }
        public Type Player1 { get; set; }
        public Type Player2 { get; set; }

    }
}
