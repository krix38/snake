using System.Collections.Generic;
using System;

using static Snake.Direction;
using static Snake.CollisionType;

namespace Snake
{
    public class Player
    {
        private const int INITIAL_LENGTH = 5;
        private const Direction INITIAL_DIRECTION = RIGHT;
        private const int INITIAL_SPEED = 10;
        private int headX, headY, tailX, tailY;
        private Arena arena;
        public int Length { get; set; }
        private Direction direction { get; set; }
        private bool isMooving = false;
        public Direction Direction { 
            get
            {
                return this.direction;
            } 
            set
            {
                if(isMooving)
                {
                    return;
                }
                switch(value)
                {
                    case UP:
                        if(this.direction != DOWN)
                            this.direction = value;
                        break;
                    case DOWN:
                        if(this.direction != UP)
                            this.direction = value;
                        break;
                    case LEFT:
                        if(this.direction != RIGHT)
                            this.direction = value;
                        break;
                    case RIGHT:
                        if(this.direction != LEFT)
                            this.direction = value;
                        break;
                }
                isMooving = true;
            } 
        }
        public bool Alive { get; set; }
        public int Speed { get; set; }
        private Queue<Direction> tailQueue = new Queue<Direction>();
        private List<Tuple<int, int>> bodyCoords = new List<Tuple<int, int>>();
        private char HEAD_CHAR = '0';
        private char BODY_CHAR = 'O';
        

    
        

        private Player()
        {
            this.Length = INITIAL_LENGTH;
            this.Direction = INITIAL_DIRECTION;
            this.Alive = true;
            this.Speed = INITIAL_SPEED;
            initializeTailQueue();
        }

        private void initializeTailQueue()
        {
            for(int i = 0; i < Length; i++)
            {
                tailQueue.Enqueue(RIGHT);
            }
        }

        public Player(int headX, int headY, Arena arena):this()
        {
            this.headX = headX;
            this.headY = headY;
            this.arena = arena;
        }

        public void InitialDrawSnakeHorizontal()
        {
            var currentX = headX;
            var currentY = headY;
            ConsoleWrapper.ConsoleWriteCharXY(HEAD_CHAR, headX, headY);
            for (int i = 0; i < this.Length; i++)
            {
                ConsoleWrapper.ConsoleWriteCharXY(BODY_CHAR, --currentX, currentY);
                bodyCoords.Add(new Tuple<int, int>(currentX, currentY));
            }
            tailX = currentX;
            tailY = currentY;
            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }

        public void Move()
        {
            ConsoleWrapper.ConsoleWriteCharXY(BODY_CHAR, headX, headY);
            bodyCoords.Add(new Tuple<int, int>(headX, headY));
            moveSnake();
            checkWallCollision(headX, headY);
            checkSelfCollision(headX, headY);
            isMooving = false;
        }

        private void moveSnake()
        {
            switch(direction)
            {
                case RIGHT:
                    moveRight();
                    break;
                case LEFT:
                    moveLeft();
                    break;
                case UP:
                    moveUp();
                    break;
                case DOWN:
                    moveDown();
                    break;
            }
        }

        private void checkWallCollision(int headX, int headY)
        {
            switch(arena.CheckCollision(headX, headY, direction))
            {
                case NONE:
                    moveTail();
                    break;
                case WALL:
                    this.Alive = false;
                    break;
                case FOOD:
                    this.Speed+=2;
                    this.Length++;
                    break;
            }
        }
        private void checkSelfCollision(int x, int y)
        {
            foreach(var coords in bodyCoords.ToArray())
            {
                if(x == coords.Item1 && y == coords.Item2)
                {
                    this.Alive = false;
                }
            }
        }

        private void removeBodyCoords(int x, int y)
        {
            foreach(var coords in bodyCoords.ToArray())
            {
                if(x == coords.Item1 && y == coords.Item2)
                {
                    bodyCoords.Remove(coords);
                }
            }            
        }

        private void moveTail()
        {
            ConsoleWrapper.ConsoleWriteCharXY(' ', tailX, tailY);
            removeBodyCoords(tailX, tailY);
            switch(tailQueue.Dequeue())
            {
                case RIGHT:
                    tailX++;
                    break;
                case LEFT:
                    tailX--;
                    break;
                case UP:
                    tailY--;
                    break;
                case DOWN:
                    tailY++;
                    break;
            }
        }

        private void moveRight()
        {
            ConsoleWrapper.ConsoleWriteCharXY(HEAD_CHAR, ++headX, headY);
            tailQueue.Enqueue(RIGHT);
        }

        private void moveLeft()
        {
            ConsoleWrapper.ConsoleWriteCharXY(HEAD_CHAR, --headX, headY);
            tailQueue.Enqueue(LEFT);
        }

        private void moveUp()
        {
            ConsoleWrapper.ConsoleWriteCharXY(HEAD_CHAR, headX, --headY);
            tailQueue.Enqueue(UP);
        }

        private void moveDown()
        {
            ConsoleWrapper.ConsoleWriteCharXY(HEAD_CHAR, headX, ++headY);
            tailQueue.Enqueue(DOWN);
        }

    }
}
