using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Players
{
    internal class GradualPlayer : AbstractPlayer
    {
        private int enemyDefeted = 0;
        private int gradRounds = 0;
        protected override async Task<bool> GetTip()
        {
            if(gradRounds > 0)
            {
                gradRounds--;
                return gradRounds > 1;
            }

            if (lastResult == null)
                return false;


            if (lastResult.Player1Tip != lastResult.Player2Tip && gradRounds == 0)
            {
                enemyDefeted++;
                gradRounds = enemyDefeted + 2;
                return await GetTip();
            }

            return false;
        }

        protected override Task Initialize(InitializePlayerMessage message)
        {
            if (message.Data == null)
                return Task.CompletedTask;


            var orderedData = message.Data.OrderBy(e => e.Round);
            for (int i = 0; i < orderedData.Count(); i++)
            {
                var item = orderedData.ElementAt(i);
                if(item.Player1Tip != item.Player2Tip && gradRounds == 0)
                {
                    enemyDefeted++;
                    gradRounds = enemyDefeted + 2;
                }
                else
                {
                    gradRounds--;
                }
            }
            return Task.CompletedTask;
        }
    }
}
