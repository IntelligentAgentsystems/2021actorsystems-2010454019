using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class SetupQueueMessage
    {
        public List<string> GameIds { get; private set; }

        public string Hostname { get; private set; }
        public int Port { get; private set; }

        public SetupQueueMessage(List<string> gameIds, string hostname, int port)
        {
            GameIds = gameIds;
            Hostname = hostname;
            Port = port;
        }
    }
}
