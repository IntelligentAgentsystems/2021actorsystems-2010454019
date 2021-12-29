using Akka.Actor;
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
            player1 = Context.ActorOf(Props.Create(message.Player1), $"P1_{nameof(message.Player1)}_{Guid.NewGuid()}");
            player2 = Context.ActorOf(Props.Create(message.Player2), $"P2_{nameof(message.Player2)}_{Guid.NewGuid()}");

            var initP1 =  player1.Ask<InitializeFinishedMessage>(new InitializePlayerMessage() { PlayerNr = 1, Data = message.Data });
            var initP2 =  player2.Ask<InitializeFinishedMessage>(new InitializePlayerMessage() { PlayerNr = 2, Data = message.Data });

            await initP1;
            await initP2;

            previousResult = message.Data?.OrderByDescending(e => e.Round).Select(e => new RoundResultMessage()
            {
                Player1Tip = e.Player1Tip,
                Player2Tip = e.Player2Tip,
                Player1Result = e.Player1Result,
                Player2Result = e.Player2Result
            }).FirstOrDefault();

            Sender.Tell(new InitializeFinishedMessage());
        }

        private async Task OnReveice_StartRoundMessage(StartRoundMessage message)
        {
            var p1 = player1.Ask<TipMessage>(new GetTipMessage() { PreviousResult = previousResult });
            var p2 = player2.Ask<TipMessage>(new GetTipMessage() { PreviousResult = previousResult });

            var p1Tip = (await p1).Tip;
            var p2Tip = (await p2).Tip;

            var result = Evaluate(p1Tip,p2Tip);

            previousResult = new RoundResultMessage()
            {
                Player1Tip = p1Tip,
                Player1Result = result.resP1,
                Player2Tip = p2Tip,
                Player2Result = result.resP2
            };

            Sender.Tell(previousResult) ;
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
