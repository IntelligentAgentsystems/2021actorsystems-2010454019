using Akka.Actor;
using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Players
{
    internal class RandomPlayer : AbstractPlayer
    {
        protected override async Task<bool> GetTip()
        {
            return await Task.FromResult(new System.Random().NextDouble() > 0.5);
        }
    }
}
