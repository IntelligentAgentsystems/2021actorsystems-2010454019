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

        public SetupQueueMessage(List<string> gameIds)
        {
            GameIds = gameIds;
        }
    }
}
