using System;

namespace BitcoinLib.Auxiliary
{
    public class ConsoleVisualizations
    {
        private static Int16 _counter = 0;

        public static void TurnSpiner()
        {
            switch (++_counter % 4)
            {
                case 0:
                    Console.Write("/");
                    break;
                case 1:
                    Console.Write("-");
                    break;
                case 2:
                    Console.Write("\\");
                    break;
                case 3:
                    Console.Write("|");
                    break;
            }
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        }
    }
}
