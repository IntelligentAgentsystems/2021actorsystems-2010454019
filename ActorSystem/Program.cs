using ActorSystems.Messages_;
using Akka.Actor;
using System;

namespace ActorSystems
{
    class Program
    {
        static void Main(string[] args)
        {         
            var system = ActorSystem.Create("MySystem");

            var print = system.ActorOf(Props.Create<PrintActor>(), "Main");


            var globalOperator = system.ActorOf(Props.Create<Operator>(), "Operator");
            globalOperator.Tell(new InitMessage());
            globalOperator.Tell(new InitializeGamesMessage(10, 4));


            Console.ReadKey();
        }
    }
}
