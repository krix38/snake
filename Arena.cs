using System.Collections.Generic;
using System;

using static Snake.Direction;
using static Snake.CollisionType;

namespace Snake 
{
    public class Arena
    {
        private const char FOOD_CHARACTER = '$';
        public void spawnFood()
        {
            Random random = new Random();
            var foodX = random.Next(1, Console.WindowWidth - 1);
            var foodY = random.Next(1, Console.WindowHeight -2 );
            foodList.Add(new Tuple<int, int>(foodX, foodY));
            ConsoleWrapper.ConsoleWriteCharXY(FOOD_CHARACTER, foodX, foodY);
        }

        private List<Tuple<int, int>> foodList = new List<Tuple<int, int>>();

        private void drawHorizontalLine(int startx, int starty)
        {
            Console.SetCursorPosition(startx, starty);
            for(var i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write("#");
            }
        }

        private void drawVerticalLine(int startx, int starty)
        {
            Console.SetCursorPosition(startx, starty);
            for(var i = 0; i < Console.WindowHeight - 1; i++)
            {
                Console.Write("#");
                Console.SetCursorPosition(startx, ++starty);
            }
        }

        private void drawBorder()
        {
            Console.Clear();
            drawHorizontalLine(0,0);
            drawVerticalLine(0,0);
            drawHorizontalLine(0, Console.WindowHeight - 2);
            drawVerticalLine(Console.WindowWidth, 0);
        }

        public void DrawArena()
        {
            drawBorder();
        }

        public CollisionType CheckCollision(int headX, int headY, Direction direction)
        {
            switch(direction)
            {
                case UP:
                    if(headY == 0)
                    {
                        return WALL;
                    }
                    break;
                case DOWN:
                    if(headY == Console.WindowHeight - 2)
                    {
                        return WALL;
                    }
                    break;
                case LEFT:
                    if(headX == 0)
                    {
                        return WALL;
                    }
                    break;
                case RIGHT:
                    if(headX == Console.WindowWidth)
                    {
                        return WALL;
                    }
                    break;
            }
            if(gotFood(headX, headY))
            {
                return FOOD;
            }
            return NONE;
        }

        private bool gotFood(int x, int y)
        {
            foreach(var food in foodList.ToArray())
            {
                if(food.Item1 == x && food.Item2 == y)
                {
                    foodList.Remove(food);
                    return true;
                }
            }
            return false;
        }
    }
}