using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrisonersDilemma.Helper
{
    internal class Utils
    {
        public static TimeSpan Timeout_GameOperator_StartGames(int games, int rounds) => TimeSpan.FromMilliseconds(games * rounds * 5000);
        public static TimeSpan Timeout_GameManager_GameStart(int rounds) => TimeSpan.FromMilliseconds(rounds*1000);

        public static TimeSpan Timeout_Playground_Initialize = TimeSpan.FromMilliseconds(5000);
        public static TimeSpan Timeout_Playground_StartRound = TimeSpan.FromMilliseconds(5000);

        public static TimeSpan Timeout_Player_Initialize = TimeSpan.FromMilliseconds(5000);
        public static TimeSpan Timeout_Player_GetTip = TimeSpan.FromMilliseconds(5000);

        public static TimeSpan Timeout_Writer_Initialize = TimeSpan.FromMilliseconds(5000);
        public static TimeSpan Timeout_Writer_Result = TimeSpan.FromMilliseconds(5000);

        public static TimeSpan Timeout_Reader_Initialize = TimeSpan.FromMilliseconds(5000);
        public static TimeSpan Timeout_Reader_GetData = TimeSpan.FromMilliseconds(5000);


        public static void MayFail()
        {
            if (new Random().NextDouble() > 0.8)
                throw new Exception("MayFail-Triggered");

            if (new Random().NextDouble() > 0.6)
                Thread.Sleep(1000);
        }
    }
}
