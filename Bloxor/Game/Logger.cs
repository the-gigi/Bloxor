using System;

namespace Bloxor.Game
{
    public class Logger
    {
        const string Prefix = "[GGG]";
            
        public static void Log(object o)
        {
            Console.WriteLine($"{Prefix} {o}");
        }
    }
}