using Akka.Actor;
using PrisonersDilemma.Helper;
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
            Sender.Tell(await Try<TipMessage>.Of(async () =>
            {
                Utils.MayFail();
                lastResult = message.PreviousResult;
                return new TipMessage(await GetTip());
            }));
        }


        private async Task OnReceive_InitializePlayerMessage(InitializePlayerMessage message)
        {           
            Sender.Tell(await Try<InitializeFinishedMessage>.Of(async () =>
            {
                Utils.MayFail();
                playerNr = message.PlayerNr;
                await Initialize(message);
                return InitializeFinishedMessage.Instance;
            }));
        }

        protected abstract Task<bool> GetTip();
        protected virtual Task Initialize(InitializePlayerMessage message) => Task.CompletedTask;
        protected RoundResultMessage lastResult;
        protected int playerNr;
    }
}
