﻿using Akka.Actor;
using PrisonersDilemma.Helper;
using PrisonersDilemma.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma
{
    internal class Writer : ReceiveActor
    {

        public Writer()
        {
            ReceiveAsync<ResultMessage>(OnReceive_ResultMessage);
        }

        private async Task OnReceive_ResultMessage(ResultMessage message)
        {
            DummyStorage.Instance.AddData(message);
            Console.WriteLine($"{message.IdGame}-{message.Round}-{message.Player1Result}-{message.Player2Result}");
            Sender.Tell(new FinishedMessage());
        }
    }
}
