using Akka.Actor;
using PrisonersDilemma.Messages;
using PrisonersDilemma.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma
{
    internal class GameManager : ReceiveActor
    {
        private IActorRef? playground;
        private IActorRef? writer;

        private int maxTries = 10;
        private int tries = 0;
        public GameManager()
        {
            ReceiveAsync<GameStartMessage>(OnReceive_GameStartMessage);
        }

        private async Task OnReceive_GameStartMessage(GameStartMessage message)
        {
            if(playground == null)
                playground = Context.ActorOf<Playground>($"{nameof(Playground)}_{Guid.NewGuid()}");

            if (writer == null)
                writer = Context.ActorOf<Writer>($"{nameof(Writer)}_{Guid.NewGuid()}");

            try
            {
                await playground.Ask<InitializeFinishedMessage>(new InitializePlaygroundMessage()
                {
                    Player1 = message.Player1,
                    Player2 = message.Player2,
                    Data = null
                }, TimeSpan.FromSeconds(5));
            }
            catch (AskTimeoutException e)
            {
                playground = null;
                Self.Tell(message);
            }


            int i = 0;
            while( i < message.Rounds)
            {
                try
                {
                    var result = await playground.Ask<RoundResultMessage>(new StartRoundMessage());
                    writer.Tell(new ResultMessage() 
                    { 
                        IdGame = message.IdGame, 
                        Round = i, 
                        Player1Result = result.Player1Result, 
                        Player1Tip = result.Player1Tip, 
                        Player2Result = result.Player2Result, 
                        Player2Tip = result.Player2Tip
                    });
                    i++;
                }
                catch (AskTimeoutException e)
                {
                    
                }
            }
        }
    }
}
