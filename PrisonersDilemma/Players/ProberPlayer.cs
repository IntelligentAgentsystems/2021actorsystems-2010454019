using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Players
{
    internal class ProberPlayer : AbstractPlayer
    {
        private bool doAlawaysDefect = true;
        protected override async Task<bool> GetTip()
        {
            if (round == 1)
                return false;

            if(round == 2)
                return true;

            if(round == 3 || round == 4)
            {
                doAlawaysDefect = doAlawaysDefect && !EnemyLastTip.Value;
            }

            if (round == 3)
                return true;

            return doAlawaysDefect ? true : EnemyLastTip.Value;

        }

        protected override Task Initialize(InitializePlayerMessage message)
        {
            var orderedData = message.Data.OrderBy(e => e.Round);
            for(int i = 0; i < Math.Min(3,round);i++)
            {
                if (round == 3 || round == 4)
                {
                    var roundResult = orderedData.ElementAt(i);
                    doAlawaysDefect = doAlawaysDefect && !(playerNr == 1 ? roundResult.Player2Tip : roundResult.Player1Tip);
                }
            }
            return base.Initialize(message);
        }
    }
}
