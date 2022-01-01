using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Players
{
    internal class MistrustPlayer : AbstractPlayer
    {
        protected override async Task<bool> GetTip()
        {
            return round == 1 ? true : EnemyLastTip.Value;
        }
    }
}
