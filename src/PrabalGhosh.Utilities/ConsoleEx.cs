using System;

namespace PrabalGhosh.Utilities
{
    public static class ConsoleEx
    {
        public static void WriteMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public static void WriteError(string msg)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = color;
        }

        public static void WriteWarning(string msg)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ForegroundColor = color;
        }
    }
}