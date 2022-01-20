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
int rounds = 100;
int gamesPerPlayer = 9;

//create gameProperties

var playerList = new[] { 
    typeof(AlwaysCooperate), 
    typeof(AlwaysDefectPlayer), 
    typeof(GradualPlayer),
    typeof(MistrustPlayer),
    typeof(PavlovPlayer),
    typeof(ProberPlayer),
    typeof(RandomPlayer),
    typeof(SpitePlayer),
    typeof(TitForTatPlayer)
};


var props = new List<GameProperties>();
for (int i = 0; i < gamesPerPlayer; i++)
{
    for (int k = 0; k < playerList.Length; k++)
    {
        props.Add(new GameProperties() { IdGame = $"{i}-{k}", Rounds = rounds, Player1 = playerList[i % playerList.Length], Player2 = playerList[k%playerList.Length] });
    }
}


var globalOperator = system.ActorOf(Props.Create<GameOperator>(), "Operator");

var results = new List<Try<GameDataMessage>>();

results.Add(await globalOperator.Ask<Try<GameDataMessage>>(new StartGamesMessage()
{
    Properties = props.ToArray(),
    Hostname = "localhost",
    Port = 5672
}, Utils.Timeout_GameOperator_StartGames(games: gamesPerPlayer* playerList.Length, rounds: rounds)));


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
