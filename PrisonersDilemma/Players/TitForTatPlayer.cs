using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Players
{
    internal class TitForTatPlayer : AbstractPlayer
    {
        protected override async Task<bool> GetTip()
        {
            return lastResult == null ? true : playerNr == 1 ? lastResult.Player2Tip : lastResult.Player1Tip;               
        }

        protected override async Task Initialize(InitializePlayerMessage message)
        {
            lastResult = message.Data.OrderByDescending(e => e.Round).Select(e => new RoundResultMessage()
            {
                Player1Tip = e.Player1Tip,
                Player2Tip = e.Player2Tip,
                Player1Result = e.Player1Result,
                Player2Result = e.Player2Result
            }).FirstOrDefault();
        }
    }
}
