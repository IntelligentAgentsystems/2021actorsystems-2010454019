using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class DeleteQueueMessage
    {
        public List<string> GameIds { get; private set; }

        public DeleteQueueMessage(List<string> gameIds)
        {
            GameIds = gameIds;
        }
    }
}
