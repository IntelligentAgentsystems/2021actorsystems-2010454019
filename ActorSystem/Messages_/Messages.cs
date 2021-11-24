using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorSystems.Messages_
{
    public struct InitMessage { };

    public struct InitializeGamesMessage
    {
        public int NumberOfGames;
        public int NumberOfRounds;

        public InitializeGamesMessage(int numberOfRounds, int numberOfGames) : this()
        {
            this.NumberOfRounds = numberOfRounds;
            this.NumberOfGames = numberOfGames;
        }
    }

    public struct StartGamesMessage
    {
        public IActorRef PlaygroundSystem;
        public IActorRef PlayerSystem;
        public IActorRef DataSystem;

        public InitializeGamesMessage GameInfo;

        public StartGamesMessage(IActorRef playgroundSystem, IActorRef playerSystem, IActorRef dataSystem, InitializeGamesMessage gameInfo)
        {
            this.PlaygroundSystem = playgroundSystem;
            this.PlayerSystem = playerSystem;
            this.DataSystem = dataSystem;
            this.GameInfo = gameInfo;
        }
    }

    public struct StartGameMessage
    {
        public IActorRef PlaygroundSystem;
        public IActorRef PlayerSystem;
        public IActorRef DataSystem;

        public int IdGame;
        public int NrOfRounds;

        public StartGameMessage(IActorRef playgroundSystem, IActorRef playerSystem, IActorRef dataSystem, int idGame, int nrOfRounds)
        {
            PlaygroundSystem = playgroundSystem;
            PlayerSystem = playerSystem;
            DataSystem = dataSystem;
            IdGame = idGame;
            NrOfRounds = nrOfRounds;
        }
    }

    public struct PlayersMessage
    {
        public IActorRef[] Players;

        public PlayersMessage(IActorRef[] players)
        {
            this.Players = players;
        }
    }

    public struct GetPlayersMessage 
    {
        public int Count;

        public GetPlayersMessage(int count)
        {
            this.Count = count;
        }
    }

    public struct PlaygroundMessage
    {
        public IActorRef Playground;

        public PlaygroundMessage(IActorRef playground)
        {
            Playground = playground;
        }
    }

    public struct GetPlaygroundMessage { }

    public struct GameMessage
    {
        public int GameId;
        public int Round;
        public bool Player1Tip;
        public bool Player2Tip;

        public GameMessage(int gameId, int round, bool player1Tip, bool player2Tip)
        {
            GameId = gameId;
            Round = round;
            Player1Tip = player1Tip;
            Player2Tip = player2Tip;
        }
    }

    public struct GetWriterMessage { }
    public struct WriterMessage 
    {
        public IActorRef Writer;

        public WriterMessage(IActorRef writer)
        {
            Writer = writer;
        }
    }

    public struct GetReaderMessage { }
    public struct ReaderMessage
    {
        public IActorRef Reader;

        public ReaderMessage(IActorRef reader)
        {
            Reader = reader;
        }
    }

    public struct GetGameDataMessage
    {
        public int GameId;

        public GetGameDataMessage(int gameId)
        {
            GameId = gameId;
        }
    }

    public struct GameDataMessage
    {
        public IList<GameMessage> GameData;

        public GameDataMessage(IList<GameMessage> gameData)
        {
            GameData = gameData;
        }
    }

    public struct RunGameMessage
    {
        public IActorRef[] Players;
        public IList<GameMessage> GameData;
        public int NrOfRounds;
        public int GameId;
        public IActorRef GameManager;

        public RunGameMessage(IActorRef[] players,int gameId, int nrOfRounds, IActorRef gameManager, IList<GameMessage> gameData)
        {
            Players = players;
            GameId = gameId;
            NrOfRounds = nrOfRounds;
            GameManager = gameManager;
            GameData = gameData;
        }
    }

    public struct TipMessage
    {
        public bool Tip;

        public TipMessage(bool tip)
        {
            Tip = tip;
        }
    }

    public struct GetTipMessage
    {
        public IEnumerable<GameMessage> GameData;

        public GetTipMessage(IEnumerable<GameMessage> gameData)
        {
            GameData = gameData;
        }
    }
}
