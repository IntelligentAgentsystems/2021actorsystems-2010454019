using Akka.Actor;
using PrisonersDilemma.Actors;
using PrisonersDilemma.Helper;
using PrisonersDilemma.Messages;
using PrisonersDilemma.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma
{
    internal class GameOperator : ReceiveActor
    {
        public GameOperator()
        {
            ReceiveAsync<StartGamesMessage>(OnReceive_StartGamesMessage);
        }

        private async Task OnReceive_StartGamesMessage(StartGamesMessage message)
        {
            Sender.Tell(await Try<GameDataMessage>.Of(async () =>
            {
                //Setup Broker
                var brokerManager = Context.ActorOf<BrokerManager>($"{nameof(BrokerManager)}_{Guid.NewGuid()}");
                (await brokerManager.Ask<Try<FinishedMessage>>(new SetupQueueMessage(message.Properties.Select(e => e.IdGame).ToList(),message.Hostname,message.Port))).OrElseThrow();


                Dictionary<string, (IActorRef actor, Task<Try<GameFinishMessage>> result)> data = new Dictionary<string, (IActorRef, Task<Try<GameFinishMessage>>)>();
                //run all games
                for (int i = 0; i < message.Properties.Count(); i++)
                {
                    var gameManager = Context.ActorOf<GameManager>($"{nameof(GameManager)}_{Guid.NewGuid()}");
                    var props = message.Properties[i];
                    data.Add(props.IdGame, (gameManager, gameManager.AskTry<GameFinishMessage>(new GameStartMessage(properties: props,hostname:message.Hostname,port:message.Port), Utils.Timeout_GameManager_GameStart(props.Rounds))));
                }

                await Task.WhenAll(data.Values.Select(e => e.result));


                //kill gamemanagers & check if game finished
                var unfinishedGameIds = new List<string>();
                foreach ((var idGame, var value) in data)
                {
                    var res = await value.result;
                    if (res.Success)
                    {
                        Console.WriteLine($"{idGame}-FINISHED");
                    }                    
                    else
                    {
                        Console.WriteLine($"{idGame}-FAILED-{res.Error.Message}");
                        unfinishedGameIds.Add(idGame);
                    }

                    Context.Stop(value.actor);
                }

                var newProps = message.Properties.Where(e => unfinishedGameIds.Contains(e.IdGame)).ToArray();
                //restart failed games
                while(unfinishedGameIds.Any())
                {
                    Thread.Sleep(1000);
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("==========================================");
                    
                    data.Clear();
                    for (int i = 0; i < newProps.Count(); i++)
                    {
                        var gameManager = Context.ActorOf<GameManager>($"{nameof(GameManager)}_{Guid.NewGuid()}");
                        var props = newProps[i];
                        data.Add(props.IdGame, (gameManager, gameManager.AskTry<GameFinishMessage>(new GameStartMessage(properties: props, hostname: message.Hostname, port: message.Port), Utils.Timeout_GameManager_GameStart(props.Rounds))));
                    }

                    await Task.WhenAll(data.Values.Select(e => e.result));

                    //kill gamemanagers & check if game finished
                    unfinishedGameIds.Clear();
                    foreach ((var idGame, var value) in data)
                    {
                        var res = await value.result;
                        if (res.Success)
                            Console.WriteLine($"{idGame}-FINISHED");
                        else
                        {
                            Console.WriteLine($"{idGame}-FAILED-{res.Error.Message}");
                            unfinishedGameIds.Add(idGame);
                        }

                        Context.Stop(value.actor);
                    }

                    newProps = newProps.Where(e => unfinishedGameIds.Contains(e.IdGame)).ToArray();
                }

                return (await brokerManager.Ask<Try<GameDataMessage>>(new DeleteQueueMessage(message.Properties.Select(e => e.IdGame).ToList()))).OrElseThrow();

            }));
        }
    }
}
