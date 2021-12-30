using Akka.Actor;
using PrisonersDilemma.Helper;
using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma
{
    internal class Playground : ReceiveActor
    {
        private IActorRef player1;
        private IActorRef player2;
        private RoundResultMessage previousResult;

        public Playground()
        {
            ReceiveAsync<InitializePlaygroundMessage>(OnReceive_InitializePlaygroundMessage);
            ReceiveAsync<StartRoundMessage>(OnReveice_StartRoundMessage);
        }

        private async Task OnReceive_InitializePlaygroundMessage(InitializePlaygroundMessage message)
        {
            Sender.Tell(await Try<InitializeFinishedMessage>.Of(async () =>
            {
                Utils.MayFail();

                player1 = Context.ActorOf(Props.Create(message.Player1), $"P1_{nameof(message.Player1)}_{Guid.NewGuid()}");
                player2 = Context.ActorOf(Props.Create(message.Player2), $"P2_{nameof(message.Player2)}_{Guid.NewGuid()}");

                var initP1 = player1.Ask<Try<InitializeFinishedMessage>>(new InitializePlayerMessage(playerNr: 1, data: message.Data),Utils.Timeout_Player_Initialize);
                var initP2 = player2.Ask<Try<InitializeFinishedMessage>>(new InitializePlayerMessage(playerNr: 2, data: message.Data), Utils.Timeout_Player_Initialize);

                (await initP1).ThrowIfFailure();
                (await initP2).ThrowIfFailure();

                previousResult = message.Data?
                .OrderByDescending(e => e.Round)
                .Select(e => new RoundResultMessage(player1Tip: e.Player1Tip, player1Result: e.Player1Result, player2Tip: e.Player2Tip, player2Result: e.Player2Result))
                .FirstOrDefault();

                return InitializeFinishedMessage.Instance;
            }));
        }

        private async Task OnReveice_StartRoundMessage(StartRoundMessage message)
        {
            Sender.Tell(await Try<RoundResultMessage>.Of(async () =>
            {
                Utils.MayFail();

                var p1 = player1.Ask<Try<TipMessage>>(new GetTipMessage(previousResult: previousResult), Utils.Timeout_Player_GetTip);
                var p2 = player2.Ask<Try<TipMessage>>(new GetTipMessage(previousResult: previousResult), Utils.Timeout_Player_GetTip);

                var p1Tip = (await p1).OrElseThrow().Tip;
                var p2Tip = (await p2).OrElseThrow().Tip;

                var result = Evaluate(p1Tip, p2Tip);

                previousResult = new RoundResultMessage(player1Tip: p1Tip, player1Result: result.resP1, player2Tip: p2Tip, player2Result: result.resP2);
                return previousResult;
            }));
        }


        private readonly int temptation = 1;
        private readonly int reward = 2;
        private readonly int punishment = 4;
        private readonly int suckersPayoff = 6;
        private (int resP1, int resP2) Evaluate(bool tipP1, bool tipP2)
        {
            if (tipP1 && tipP2)
                return (punishment, punishment);
            else if (tipP1 && !tipP2)
                return (temptation, suckersPayoff);
            else if (!tipP1 && tipP2)
                return (suckersPayoff, temptation);
            else if (!tipP1 && !tipP2)
                return (reward, reward);
            else
                throw new Exception("Case does not exist!");
        }
    }
}
