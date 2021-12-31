﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class InitializeWriterMessage
    {
        public string IdGame { get; private set; }

        public InitializeWriterMessage(string idGame)
        {
            IdGame = idGame;
        }
    }
}