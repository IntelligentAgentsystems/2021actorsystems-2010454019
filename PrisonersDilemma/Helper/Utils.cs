using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Helper
{
    internal class Utils
    {
        public static TimeSpan Timeout_GameOperator_StartGames(int games, int rounds) => TimeSpan.FromMilliseconds(games * rounds * 50000);
        public static TimeSpan Timeout_GameManager_GameStart(int rounds) => TimeSpan.FromMilliseconds(rounds*10000);

        public const string ExchangeName = "PrisonersDilemma";

        public static void MayFail()
        {
            if (new Random().NextDouble() > 0.99)
                throw new Exception("MayFail-Triggered");

            if (new Random().NextDouble() > 0.9)
                Thread.Sleep(250);
        }
    }
}
