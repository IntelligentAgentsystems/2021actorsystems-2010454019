using Akka.Actor;
using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Players
{
    internal abstract class AbstractPlayer : ReceiveActor
    {
        public AbstractPlayer()
        {
            Receive<GetTipMessage>(OnReceive_GetTipMessage);
        }
        private void OnReceive_GetTipMessage(GetTipMessage message)
        {
            Sender.Tell(new TipMessage() { Tip = GetTip() });
        }

        protected abstract bool GetTip();
    }
}
