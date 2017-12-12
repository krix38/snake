using System;

using System.Threading;

using static Snake.Direction;

namespace Snake
{
    class Program
    {
        private static int SLEEP = 100;

        static void Main(string[] args)
        {
            int spawnFoodCounter = 0;
            Arena arena = new Arena();
            arena.DrawArena();
            Player player = new Player(20, 20, arena);
            player.InitialDrawSnakeHorizontal();
            new Thread(() => {
                while(player.Alive)
                {
                    switch(Console.ReadKey().Key)
                    {
                        case ConsoleKey.UpArrow:
                            player.Direction = UP;
                            break;
                        case ConsoleKey.DownArrow:
                            player.Direction = DOWN;
                            break;
                        case ConsoleKey.LeftArrow:
                            player.Direction = LEFT;
                            break;
                        case ConsoleKey.RightArrow:
                            player.Direction = RIGHT;
                            break;
                    }
                }
            }).Start();
            while(player.Alive)
            {
                if(spawnFoodCounter == 10)
                {
                    spawnFoodCounter = 0;
                    arena.spawnFood();
                }
                spawnFoodCounter++;
                var sleepTime = SLEEP - player.Speed;
                if(player.Direction == LEFT || player.Direction == RIGHT){
                    sleepTime /= 2;
                }
                Thread.Sleep( sleepTime > 0 ? sleepTime : 10 );
                player.Move();
            }
            Console.SetCursorPosition(0, Console.WindowHeight);
        }
    }
}
