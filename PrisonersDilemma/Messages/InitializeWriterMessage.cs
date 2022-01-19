using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class InitializeWriterMessage
    {
        public string IdGame { get; private set; }
        public string Hostname { get; private set; }
        public int Port { get; private set; }

        public InitializeWriterMessage(string idGame, string hostname, int port)
        {
            IdGame = idGame;
            Hostname = hostname;
            Port = port;
        }
    }
}
