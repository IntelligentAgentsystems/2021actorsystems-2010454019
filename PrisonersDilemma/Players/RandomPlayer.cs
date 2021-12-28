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
        public RandomPlayer() : base()
        {
           
        }

        protected override bool GetTip()
        {
            return new System.Random().NextDouble() > 0.5;
        }
    }
}
