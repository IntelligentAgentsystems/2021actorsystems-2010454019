using PrisonersDilemma.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class GameStartMessage
    {
        public GameProperties Properties { get; private set; }
        public string Hostname { get; private set; }
        public int Port { get; private set; }


        public GameStartMessage(GameProperties properties, string hostname, int port)
        {
            Properties = properties;
            Hostname = hostname;
            Port = port;
        }
    }
}
