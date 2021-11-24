using ActorSystems.Messages_;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorSystems.PlaygroundSystem_
{
    public class Playground : UntypedActor
    {
        private IList<GameMessage> gameData;
        protected override void OnReceive(object message)
        {
            if (message is RunGameMessage)
            {
                var runGameMessage = (RunGameMessage)message;
                gameData = runGameMessage.GameData;

                for (int i = 0; i < runGameMessage.NrOfRounds; i++)
                {
                    var tip1 = runGameMessage.Players[0].Ask<TipMessage>(new GetTipMessage(gameData));
                    var tip2 = runGameMessage.Players[1].Ask<TipMessage>(new GetTipMessage(gameData));

                    var resTip1 = tip1.GetAwaiter().GetResult().Tip;
                    var resTip2 = tip2.GetAwaiter().GetResult().Tip;

                    var roundResult = new GameMessage(runGameMessage.GameId, i, resTip1, resTip2);
                    gameData.Add(roundResult);
                    Sender.Tell(roundResult);
                }
            }
            else
                throw new NotImplementedException();
        }
    }
}
