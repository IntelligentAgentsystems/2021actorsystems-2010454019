using Akka.Actor;
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
            Sender.Tell(await Try<FinishedMessage>.Of(async () =>
            {
                Dictionary<Guid, (IActorRef actor, Task<Try<GameFinishMessage>> result)> data = new Dictionary<Guid, (IActorRef, Task<Try<GameFinishMessage>>)>();
                //run all games
                for (int i = 0; i < message.Properties.Count(); i++)
                {
                    var gameManager = Context.ActorOf<GameManager>($"{nameof(GameManager)}_{Guid.NewGuid()}");
                    var props = message.Properties[i];
                    data.Add(props.IdGame, (gameManager, gameManager.AskTry<GameFinishMessage>(new GameStartMessage(properties: props), Utils.Timeout_GameManager_GameStart(props.Rounds))));
                }

                await Task.WhenAll(data.Values.Select(e => e.result));


                //kill gamemanagers & check if game finished
                var unfinishedGameIds = new List<Guid>();
                foreach ((var idGame, var value) in data)
                {
                    var res = await value.result;
                    if (res.Success)
                        Console.WriteLine($"{idGame}-FINISHED");
                    else
                    {
                        Console.WriteLine($"{idGame}-FAILED-{res.Error}");
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
                        data.Add(props.IdGame, (gameManager, gameManager.AskTry<GameFinishMessage>(new GameStartMessage(properties: props),Utils.Timeout_GameManager_GameStart(props.Rounds))));
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

                return FinishedMessage.Instance;
            }));
        }
    }
}
