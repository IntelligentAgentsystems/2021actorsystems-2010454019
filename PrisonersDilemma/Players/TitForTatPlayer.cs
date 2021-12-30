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
            return await Task.FromResult(lastResult == null ? true : playerNr == 1 ? lastResult.Player2Tip : lastResult.Player1Tip);               
        }

        protected override Task Initialize(InitializePlayerMessage message)
        {
            lastResult = message.Data
                .OrderByDescending(e => e.Round)
                .Select(e => new RoundResultMessage(player1Tip: e.Player1Tip, player2Tip: e.Player2Tip, player1Result: e.Player1Result, player2Result: e.Player2Result))
                .FirstOrDefault();

            return Task.CompletedTask;
        }
    }
}
