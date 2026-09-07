using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD.Game
{
    public static class GameLogger
    {
        private static string filePath = Path.Combine(
            AppContext.BaseDirectory,
            "gameLog.txt"
        );

        public static void StartNewLog()
        {
            File.WriteAllText(filePath, "");
        }

        public static void Log(string message)
        {
            Console.WriteLine(message);

            File.AppendAllText(
                filePath,
                message + Environment.NewLine
            );
        }
    }
}
