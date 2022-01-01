using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Players
{
    internal class SpitePlayer : AbstractPlayer
    {
        protected override async Task<bool> GetTip()
        {
            return lastResult == null ? false : MyLastTip.Value || EnemyLastTip.Value;
        }
    }
}
