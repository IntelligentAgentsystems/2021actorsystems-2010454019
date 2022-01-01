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
                round++;
                lastResult = message.PreviousResult;
                MyLastTip = playerNr == 1 ? lastResult?.Player1Tip : lastResult?.Player2Tip;
                EnemyLastTip = playerNr == 2 ? lastResult?.Player1Tip : lastResult?.Player2Tip;
                return new TipMessage(await GetTip());
            }));
        }


        private async Task OnReceive_InitializePlayerMessage(InitializePlayerMessage message)
        {           
            Sender.Tell(await Try<InitializeFinishedMessage>.Of(async () =>
            {
                Utils.MayFail();
                playerNr = message.PlayerNr;

                var lRes = message?.Data.OrderByDescending(e => e.Round).FirstOrDefault();

                if(lRes != null)
                {
                    round = lRes.Round;
                    lastResult = new RoundResultMessage(player1Tip: lRes.Player1Tip, player2Tip: lRes.Player2Tip, player1Result: lRes.Player1Result, player2Result: lRes.Player2Result);

                    MyLastTip = playerNr == 1 ? lastResult?.Player1Tip : lastResult?.Player2Tip;
                    EnemyLastTip = playerNr == 2 ? lastResult?.Player1Tip : lastResult?.Player2Tip;
                }
             

                await Initialize(message);
                return InitializeFinishedMessage.Instance;
            }));
        }

        protected abstract Task<bool> GetTip();
        protected virtual Task Initialize(InitializePlayerMessage message) => Task.CompletedTask;
        protected RoundResultMessage lastResult;
        protected bool? MyLastTip;
        protected bool? EnemyLastTip;
        protected int playerNr;
        protected int round = 0;
    }
}
