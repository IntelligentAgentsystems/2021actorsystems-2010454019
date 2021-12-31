﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class GetDataMessage
    {
        public string IdGame { get; private set; }

        public GetDataMessage(string idGame)
        {
            IdGame = idGame;
        }
    }
}
