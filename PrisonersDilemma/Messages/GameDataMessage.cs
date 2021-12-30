using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class GameDataMessage
    {
        public IList<ResultMessage> Data { get; private set; }

        public GameDataMessage(IList<ResultMessage> data)
        {
            Data = data;
        }
    }
}
