using System;

namespace Snake
{
    public class ConsoleWrapper
    {
        public static void ConsoleWriteCharXY(char character, int consoleX, int consoleY)
        {
            Console.SetCursorPosition(consoleX, consoleY);
            Console.Write(character);
        }
    }
}