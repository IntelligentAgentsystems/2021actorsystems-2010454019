﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class GameManagerInitializeMessage
    {
        public GameManagerInitializeMessage(string idGame, string hostname, int port)
        {
            Hostname = hostname;
            Port = port;
            IdGame = idGame;
        }

        public string IdGame { get; private set; }
        public string Hostname { get; private set; }
        public int Port { get; private set; }
    }
}
