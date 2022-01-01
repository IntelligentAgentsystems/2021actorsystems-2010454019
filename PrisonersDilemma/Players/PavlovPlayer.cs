using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Players
{
    internal class PavlovPlayer : AbstractPlayer
    {
        protected override async Task<bool> GetTip()
        {
            return lastResult == null ? false : MyLastTip.Value != EnemyLastTip.Value;
        }
    }
}
