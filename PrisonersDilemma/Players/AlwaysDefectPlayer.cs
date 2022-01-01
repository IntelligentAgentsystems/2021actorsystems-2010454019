using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Players
{
    internal class AlwaysDefectPlayer : AbstractPlayer
    {
        protected override async Task<bool> GetTip()
        {
            return true;
        }
    }
}
