// See https://aka.ms/new-console-template for more information
using Akka.Actor;
using Akka.Configuration;
using PrisonersDilemma;
using PrisonersDilemma.Helper;
using PrisonersDilemma.Messages;
using PrisonersDilemma.Players;
using RabbitMQ.Client;

Console.WriteLine("Hello, World!");

var storage = DummyStorage.Instance;

BootstrapSetup Bootstrap = BootstrapSetup.Create().WithConfig(
    ConfigurationFactory.ParseString(@"
    akka{
        loglevel = OFF
        log-dead-letters = off
        actor{
            serialize-messages = off
            
            
        }
    }
"));

var system = ActorSystem.Create("MySystem", Bootstrap);
int rounds = 10;


var globalOperator = system.ActorOf(Props.Create<GameOperator>(), "Operator");

var results = new List<Try<GameDataMessage>>();

results.Add(await globalOperator.Ask<Try<GameDataMessage>>(new StartGamesMessage()
{
    Properties = new GameProperties[]
        {
           new GameProperties() {IdGame = "a-a", Rounds = rounds, Player1 = typeof(AlwaysCooperate),Player2 = typeof(AlwaysCooperate) },
           new GameProperties() {IdGame = "a-b", Rounds = rounds, Player1 = typeof(AlwaysCooperate),Player2 = typeof(AlwaysCooperate) },
           new GameProperties() {IdGame = "a-c", Rounds = rounds, Player1 = typeof(AlwaysCooperate),Player2 = typeof(AlwaysCooperate) },
           new GameProperties() {IdGame = "a-d", Rounds = rounds, Player1 = typeof(AlwaysCooperate),Player2 = typeof(AlwaysCooperate) },
           new GameProperties() {IdGame = "a-e", Rounds = rounds, Player1 = typeof(AlwaysCooperate),Player2 = typeof(AlwaysCooperate) },
           new GameProperties() {IdGame = "a-f", Rounds = rounds, Player1 = typeof(AlwaysCooperate),Player2 = typeof(AlwaysCooperate) },
           new GameProperties() {IdGame = "a-g", Rounds = rounds, Player1 = typeof(AlwaysCooperate),Player2 = typeof(AlwaysCooperate) },
           new GameProperties() {IdGame = "a-h", Rounds = rounds, Player1 = typeof(AlwaysCooperate),Player2 = typeof(AlwaysCooperate) },
           new GameProperties() {IdGame = "a-i", Rounds = rounds, Player1 = typeof(AlwaysCooperate),Player2 = typeof(AlwaysCooperate) },

           //new GameProperties() {IdGame = "b-a", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(AlwaysCooperate) },
           //new GameProperties() {IdGame = "b-b", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(AlwaysDefectPlayer) },
           //new GameProperties() {IdGame = "b-c", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(GradualPlayer) },
           //new GameProperties() {IdGame = "b-d", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(MistrustPlayer) },
           //new GameProperties() {IdGame = "b-e", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(PavlovPlayer) },
           //new GameProperties() {IdGame = "b-f", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(ProberPlayer) },
           //new GameProperties() {IdGame = "b-g", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(RandomPlayer) },
           //new GameProperties() {IdGame = "b-h", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(SpitePlayer) },
           //new GameProperties() {IdGame = "b-i", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(TitForTatPlayer) },

           //new GameProperties() {IdGame = "c-a", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(AlwaysCooperate) },
           //new GameProperties() {IdGame = "c-b", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(AlwaysDefectPlayer) },
           //new GameProperties() {IdGame = "c-c", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(GradualPlayer) },
           //new GameProperties() {IdGame = "c-d", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(MistrustPlayer) },
           //new GameProperties() {IdGame = "c-e", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(PavlovPlayer) },
           //new GameProperties() {IdGame = "c-f", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(ProberPlayer) },
           //new GameProperties() {IdGame = "c-g", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(RandomPlayer) },
           //new GameProperties() {IdGame = "c-h", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(SpitePlayer) },
           //new GameProperties() {IdGame = "c-i", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(TitForTatPlayer) },

           //new GameProperties() {IdGame = "d-a", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(AlwaysCooperate) },
           //new GameProperties() {IdGame = "d-b", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(AlwaysDefectPlayer) },
           //new GameProperties() {IdGame = "d-c", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(GradualPlayer) },
           //new GameProperties() {IdGame = "d-d", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(MistrustPlayer) },
           //new GameProperties() {IdGame = "d-e", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(PavlovPlayer) },
           //new GameProperties() {IdGame = "d-f", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(ProberPlayer) },
           //new GameProperties() {IdGame = "d-g", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(RandomPlayer) },
           //new GameProperties() {IdGame = "d-h", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(SpitePlayer) },
           //new GameProperties() {IdGame = "d-i", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(TitForTatPlayer) },

           //new GameProperties() {IdGame = "e-a", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(AlwaysCooperate) },
           //new GameProperties() {IdGame = "e-b", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(AlwaysDefectPlayer) },
           //new GameProperties() {IdGame = "e-c", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(GradualPlayer) },
           //new GameProperties() {IdGame = "e-d", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(MistrustPlayer) },
           //new GameProperties() {IdGame = "e-e", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(PavlovPlayer) },
           //new GameProperties() {IdGame = "e-f", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(ProberPlayer) },
           //new GameProperties() {IdGame = "e-g", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(RandomPlayer) },
           //new GameProperties() {IdGame = "e-h", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(SpitePlayer) },
           //new GameProperties() {IdGame = "e-i", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(TitForTatPlayer) },

           //new GameProperties() {IdGame = "f-a", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(AlwaysCooperate) },
           //new GameProperties() {IdGame = "f-b", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(AlwaysDefectPlayer) },
           //new GameProperties() {IdGame = "f-c", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(GradualPlayer) },
           //new GameProperties() {IdGame = "f-d", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(MistrustPlayer) },
           //new GameProperties() {IdGame = "f-e", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(PavlovPlayer) },
           //new GameProperties() {IdGame = "f-f", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(ProberPlayer) },
           //new GameProperties() {IdGame = "f-g", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(RandomPlayer) },
           //new GameProperties() {IdGame = "f-h", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(SpitePlayer) },
           //new GameProperties() {IdGame = "f-i", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(TitForTatPlayer) },

           //new GameProperties() {IdGame = "g-a", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(AlwaysCooperate) },
           //new GameProperties() {IdGame = "g-b", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(AlwaysDefectPlayer) },
           //new GameProperties() {IdGame = "g-c", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(GradualPlayer) },
           //new GameProperties() {IdGame = "g-d", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(MistrustPlayer) },
           //new GameProperties() {IdGame = "g-e", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(PavlovPlayer) },
           //new GameProperties() {IdGame = "g-f", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(ProberPlayer) },
           //new GameProperties() {IdGame = "g-g", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
           //new GameProperties() {IdGame = "g-h", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(SpitePlayer) },
           //new GameProperties() {IdGame = "g-i", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(TitForTatPlayer) },

           //new GameProperties() {IdGame = "h-a", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(AlwaysCooperate) },
           //new GameProperties() {IdGame = "h-b", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(AlwaysDefectPlayer) },
           //new GameProperties() {IdGame = "h-c", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(GradualPlayer) },
           //new GameProperties() {IdGame = "h-d", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(MistrustPlayer) },
           //new GameProperties() {IdGame = "h-e", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(PavlovPlayer) },
           //new GameProperties() {IdGame = "h-f", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(ProberPlayer) },
           //new GameProperties() {IdGame = "h-g", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(RandomPlayer) },
           //new GameProperties() {IdGame = "h-h", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(SpitePlayer) },
           //new GameProperties() {IdGame = "h-i", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(TitForTatPlayer) },

           //new GameProperties() {IdGame = "i-a", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(AlwaysCooperate) },
           //new GameProperties() {IdGame = "i-b", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(AlwaysDefectPlayer) },
           //new GameProperties() {IdGame = "i-c", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(GradualPlayer) },
           //new GameProperties() {IdGame = "i-d", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(MistrustPlayer) },
           //new GameProperties() {IdGame = "i-e", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(PavlovPlayer) },
           //new GameProperties() {IdGame = "i-f", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(ProberPlayer) },
           //new GameProperties() {IdGame = "i-g", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(RandomPlayer) },
           //new GameProperties() {IdGame = "i-h", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(SpitePlayer) },
           //new GameProperties() {IdGame = "i-i", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(TitForTatPlayer) },

        },
    Hostname = "localhost",
    Port = 5672
}, Utils.Timeout_GameOperator_StartGames(games: 9, rounds: rounds)));

//results.Add(await globalOperator.Ask<Try<GameDataMessage>>(new StartGamesMessage()
//{
//    Properties = new GameProperties[]
//        {
//           new GameProperties() {IdGame = "b-a", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(AlwaysCooperate) },
//           new GameProperties() {IdGame = "b-b", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(AlwaysDefectPlayer) },
//           new GameProperties() {IdGame = "b-c", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(GradualPlayer) },
//           new GameProperties() {IdGame = "b-d", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(MistrustPlayer) },
//           new GameProperties() {IdGame = "b-e", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(PavlovPlayer) },
//           new GameProperties() {IdGame = "b-f", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(ProberPlayer) },
//           new GameProperties() {IdGame = "b-g", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(RandomPlayer) },
//           new GameProperties() {IdGame = "b-h", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(SpitePlayer) },
//           new GameProperties() {IdGame = "b-i", Rounds = rounds, Player1 = typeof(AlwaysDefectPlayer),Player2 = typeof(TitForTatPlayer) },
//        },
//    Hostname = "localhost",
//    Port = 5672
//}, Utils.Timeout_GameOperator_StartGames(games: 9, rounds: rounds)));

//results.Add(await globalOperator.Ask<Try<GameDataMessage>>(new StartGamesMessage()
//{
//    Properties = new GameProperties[]
//        {

//           new GameProperties() {IdGame = "c-a", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(AlwaysCooperate) },
//           new GameProperties() {IdGame = "c-b", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(AlwaysDefectPlayer) },
//           new GameProperties() {IdGame = "c-c", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(GradualPlayer) },
//           new GameProperties() {IdGame = "c-d", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(MistrustPlayer) },
//           new GameProperties() {IdGame = "c-e", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(PavlovPlayer) },
//           new GameProperties() {IdGame = "c-f", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(ProberPlayer) },
//           new GameProperties() {IdGame = "c-g", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(RandomPlayer) },
//           new GameProperties() {IdGame = "c-h", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(SpitePlayer) },
//           new GameProperties() {IdGame = "c-i", Rounds = rounds, Player1 = typeof(GradualPlayer),Player2 = typeof(TitForTatPlayer) },

//        },
//    Hostname = "localhost",
//    Port = 5672
//}, Utils.Timeout_GameOperator_StartGames(games: 9, rounds: rounds)));

//results.Add(await globalOperator.Ask<Try<GameDataMessage>>(new StartGamesMessage()
//{
//    Properties = new GameProperties[]
//        {
//           new GameProperties() {IdGame = "d-a", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(AlwaysCooperate) },
//           new GameProperties() {IdGame = "d-b", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(AlwaysDefectPlayer) },
//           new GameProperties() {IdGame = "d-c", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(GradualPlayer) },
//           new GameProperties() {IdGame = "d-d", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(MistrustPlayer) },
//           new GameProperties() {IdGame = "d-e", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(PavlovPlayer) },
//           new GameProperties() {IdGame = "d-f", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(ProberPlayer) },
//           new GameProperties() {IdGame = "d-g", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(RandomPlayer) },
//           new GameProperties() {IdGame = "d-h", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(SpitePlayer) },
//           new GameProperties() {IdGame = "d-i", Rounds = rounds, Player1 = typeof(MistrustPlayer),Player2 = typeof(TitForTatPlayer) },

//        },
//    Hostname = "localhost",
//    Port = 5672
//}, Utils.Timeout_GameOperator_StartGames(games: 9, rounds: rounds)));

//results.Add(await globalOperator.Ask<Try<GameDataMessage>>(new StartGamesMessage()
//{
//    Properties = new GameProperties[]
//        {
//           new GameProperties() {IdGame = "e-a", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(AlwaysCooperate) },
//           new GameProperties() {IdGame = "e-b", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(AlwaysDefectPlayer) },
//           new GameProperties() {IdGame = "e-c", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(GradualPlayer) },
//           new GameProperties() {IdGame = "e-d", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(MistrustPlayer) },
//           new GameProperties() {IdGame = "e-e", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(PavlovPlayer) },
//           new GameProperties() {IdGame = "e-f", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(ProberPlayer) },
//           new GameProperties() {IdGame = "e-g", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(RandomPlayer) },
//           new GameProperties() {IdGame = "e-h", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(SpitePlayer) },
//           new GameProperties() {IdGame = "e-i", Rounds = rounds, Player1 = typeof(PavlovPlayer),Player2 = typeof(TitForTatPlayer) },

//        },
//    Hostname = "localhost",
//    Port = 5672
//}, Utils.Timeout_GameOperator_StartGames(games: 9, rounds: rounds)));

//results.Add(await globalOperator.Ask<Try<GameDataMessage>>(new StartGamesMessage()
//{
//    Properties = new GameProperties[]
//        {
//           new GameProperties() {IdGame = "f-a", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(AlwaysCooperate) },
//           new GameProperties() {IdGame = "f-b", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(AlwaysDefectPlayer) },
//           new GameProperties() {IdGame = "f-c", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(GradualPlayer) },
//           new GameProperties() {IdGame = "f-d", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(MistrustPlayer) },
//           new GameProperties() {IdGame = "f-e", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(PavlovPlayer) },
//           new GameProperties() {IdGame = "f-f", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(ProberPlayer) },
//           new GameProperties() {IdGame = "f-g", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(RandomPlayer) },
//           new GameProperties() {IdGame = "f-h", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(SpitePlayer) },
//           new GameProperties() {IdGame = "f-i", Rounds = rounds, Player1 = typeof(ProberPlayer),Player2 = typeof(TitForTatPlayer) },

//        },
//    Hostname = "localhost",
//    Port = 5672
//}, Utils.Timeout_GameOperator_StartGames(games: 9, rounds: rounds)));

//results.Add(await globalOperator.Ask<Try<GameDataMessage>>(new StartGamesMessage()
//{
//    Properties = new GameProperties[]
//        {
//           new GameProperties() {IdGame = "g-a", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(AlwaysCooperate) },
//           new GameProperties() {IdGame = "g-b", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(AlwaysDefectPlayer) },
//           new GameProperties() {IdGame = "g-c", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(GradualPlayer) },
//           new GameProperties() {IdGame = "g-d", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(MistrustPlayer) },
//           new GameProperties() {IdGame = "g-e", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(PavlovPlayer) },
//           new GameProperties() {IdGame = "g-f", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(ProberPlayer) },
//           new GameProperties() {IdGame = "g-g", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(RandomPlayer) },
//           new GameProperties() {IdGame = "g-h", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(SpitePlayer) },
//           new GameProperties() {IdGame = "g-i", Rounds = rounds, Player1 = typeof(RandomPlayer),Player2 = typeof(TitForTatPlayer) },

//        },
//    Hostname = "localhost",
//    Port = 5672
//}, Utils.Timeout_GameOperator_StartGames(games: 9, rounds: rounds)));

//results.Add(await globalOperator.Ask<Try<GameDataMessage>>(new StartGamesMessage()
//{
//    Properties = new GameProperties[]
//        {

//           new GameProperties() {IdGame = "h-a", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(AlwaysCooperate) },
//           new GameProperties() {IdGame = "h-b", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(AlwaysDefectPlayer) },
//           new GameProperties() {IdGame = "h-c", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(GradualPlayer) },
//           new GameProperties() {IdGame = "h-d", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(MistrustPlayer) },
//           new GameProperties() {IdGame = "h-e", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(PavlovPlayer) },
//           new GameProperties() {IdGame = "h-f", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(ProberPlayer) },
//           new GameProperties() {IdGame = "h-g", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(RandomPlayer) },
//           new GameProperties() {IdGame = "h-h", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(SpitePlayer) },
//           new GameProperties() {IdGame = "h-i", Rounds = rounds, Player1 = typeof(SpitePlayer),Player2 = typeof(TitForTatPlayer) },


//        },
//    Hostname = "localhost",
//    Port = 5672
//}, Utils.Timeout_GameOperator_StartGames(games: 9, rounds: rounds)));

//results.Add(await globalOperator.Ask<Try<GameDataMessage>>(new StartGamesMessage()
//{
//    Properties = new GameProperties[]
//        {
//           new GameProperties() {IdGame = "i-a", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(AlwaysCooperate) },
//           new GameProperties() {IdGame = "i-b", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(AlwaysDefectPlayer) },
//           new GameProperties() {IdGame = "i-c", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(GradualPlayer) },
//           new GameProperties() {IdGame = "i-d", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(MistrustPlayer) },
//           new GameProperties() {IdGame = "i-e", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(PavlovPlayer) },
//           new GameProperties() {IdGame = "i-f", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(ProberPlayer) },
//           new GameProperties() {IdGame = "i-g", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(RandomPlayer) },
//           new GameProperties() {IdGame = "i-h", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(SpitePlayer) },
//           new GameProperties() {IdGame = "i-i", Rounds = rounds, Player1 = typeof(TitForTatPlayer),Player2 = typeof(TitForTatPlayer) },

//        },
//    Hostname = "localhost",
//    Port = 5672
//}, Utils.Timeout_GameOperator_StartGames(games: 9, rounds: rounds)));

foreach (var item in results)
{
    if (item.Success)
    {
        foreach (var res in item.Value.Data)
        {
            Console.WriteLine($"{res.IdGame};{res.Round};{res.Player1Tip};{res.Player2Tip};{res.Player1Result};{res.Player2Result}");
        }
    }
    else
        Console.WriteLine(item.Error.Message);

}

Console.WriteLine("FIN");
Console.ReadKey();
