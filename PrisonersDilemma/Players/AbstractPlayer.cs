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
            ReceiveAsync<GetTipMessage>(OnReceive_GetTipMessage);
            ReceiveAsync<InitializePlayerMessage>(OnReceive_InitializePlayerMessage);
        }
        private async Task OnReceive_GetTipMessage(GetTipMessage message)
        {
            lastResult = message.PreviousResult;
            Sender.Tell(new TipMessage() { Tip = await GetTip() });
        }

        private async Task OnReceive_InitializePlayerMessage(InitializePlayerMessage message)
        {
            playerNr = message.PlayerNr;
            await Initialize(message);
            Sender.Tell(new InitializeFinishedMessage());
        }

        protected abstract Task<bool> GetTip();
        protected virtual Task Initialize(InitializePlayerMessage message) => Task.CompletedTask;
        protected RoundResultMessage lastResult;
        protected int playerNr;
    }
}
