using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class InitializePlaygroundMessage
    {
        public InitializePlaygroundMessage(Type player1, Type player2, IList<ResultMessage> data)
        {
            Player1 = player1;
            Player2 = player2;
            Data = data;
        }

        public Type Player1 { get; private set; }
        public Type Player2 { get; private set; }

        public IList<ResultMessage> Data { get; private set; }
    }
}
