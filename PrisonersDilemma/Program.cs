// See https://aka.ms/new-console-template for more information
using Akka.Actor;
using PrisonersDilemma;
using PrisonersDilemma.Helper;
using PrisonersDilemma.Messages;
using PrisonersDilemma.Players;

Console.WriteLine("Hello, World!");

var storage = DummyStorage.Instance;

var system = ActorSystem.Create("MySystem");

var globalOperator = system.ActorOf(Props.Create<GameOperator>(), "Operator");
await globalOperator.Ask<FinishedMessage>(new InitializeGamesMessage()
{
    Properties= new GameProperties[] 
        { 
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(TruePlayer),Player2 = typeof(TruePlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(FalsePlayer),Player2 = typeof(FalsePlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(TruePlayer),Player2 = typeof(TruePlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(FalsePlayer),Player2 = typeof(FalsePlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(TruePlayer),Player2 = typeof(TruePlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(FalsePlayer),Player2 = typeof(FalsePlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(TruePlayer),Player2 = typeof(TruePlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(FalsePlayer),Player2 = typeof(FalsePlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           new GameProperties() {IdGame = Guid.NewGuid(), Rounds = 10, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
        }
});

Console.WriteLine("FIN");
Console.ReadKey();
