using ActorSystems.Messages_;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorSystems.ManagerSystem_
{
    public class GameManager : UntypedActor
    {
        private IActorRef writer;
        private IActorRef reader;

        private int currentRound = 0;

        protected override void OnReceive(object message)
        {
            if (message is StartGameMessage)
            {
                var startGameMsg = (StartGameMessage)message;

                writer = startGameMsg.DataSystem.Ask<WriterMessage>(new GetWriterMessage()).Result.Writer;
                reader = startGameMsg.DataSystem.Ask<ReaderMessage>(new GetReaderMessage()).Result.Reader;

                var players = startGameMsg.PlayerSystem.Ask<PlayersMessage>(new GetPlayersMessage(count: 2)).GetAwaiter().GetResult().Players;
                var playground = startGameMsg.PlaygroundSystem.Ask<PlaygroundMessage>(new GetPlaygroundMessage()).GetAwaiter().GetResult().Playground;
                var gameData = reader.Ask<GameDataMessage>(new GetGameDataMessage(startGameMsg.IdGame)).GetAwaiter().GetResult();

                playground.Tell(new RunGameMessage(players,startGameMsg.IdGame,startGameMsg.NrOfRounds,Self,gameData.GameData));
                
            }
            else if(message is GameMessage)
            {
                currentRound++;
                writer.Tell(message);
            }
            else
                throw new NotImplementedException();
        }
    }
}
