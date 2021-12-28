// See https://aka.ms/new-console-template for more information
using Akka.Actor;
using PrisonersDilemma;
using PrisonersDilemma.Messages;

Console.WriteLine("Hello, World!");

var system = ActorSystem.Create("MySystem");

var globalOperator = system.ActorOf(Props.Create<GameOperator>(), "Operator");
globalOperator.Tell(new InitializeGamesMessage() { Games = 4, Rounds = 10});

Console.WriteLine("FIN");
Console.ReadKey();
