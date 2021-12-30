using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Helper
{
    internal static class Extensions
    {
        public static void TellIfNoAskTimeout<T>(this IActorRef sender,Try<T> message)
        {

            if (message.Failure && message.Error is AskTimeoutException)
                Console.WriteLine("Ask Timeout");

            else if (message.Success || message.Error is not AskTimeoutException)
                sender?.Tell(message);
        }

        public static async Task<Try<T>> AskTry<T>(this IActorRef actorref,object message,TimeSpan timeout)
        {
            try
            {
                return await actorref.Ask<Try<T>>(message,timeout);
            }
            catch (Exception ex)
            {
                return Try<T>.Of(ex);
            }
            
        }
    }
}
