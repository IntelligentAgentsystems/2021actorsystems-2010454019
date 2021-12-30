using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Messages
{
    internal class GetTipMessage
    {
        public RoundResultMessage PreviousResult { get; private set; }

        public GetTipMessage(RoundResultMessage previousResult)
        {
            PreviousResult = previousResult;
        }
    }
}
