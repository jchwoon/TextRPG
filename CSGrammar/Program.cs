using ConsoleTables;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;



namespace CSGrammar
{
    
    public class Program
    {
        
        public enum GameObjectType
        {
            GOT_None,
            GOT_Wall,
            GOT_Space,
            GOT_Snake,
            GOT_Food,
        }
        
        public static bool isFoodExist = false;
        public static bool isGameEnd = false;
        public static int MapSize;
        public static GameObjectType[,]? GameMap;

        static void PrintMap(GameObjectType[,] Map, List<(int, int)> snakeBody,int MapSize)
        {
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    if (i == 0 || j == 0 || i == MapSize - 1 || j == MapSize - 1)
                    {
                        Map[i, j] = GameObjectType.GOT_Wall;
                    }
                    else if (Map[i,j] == GameObjectType.GOT_Food)
                    {
                        Map[i, j] = GameObjectType.GOT_Food;
                    }
                    else
                    {
                        Map[i, j] = GameObjectType.GOT_Space;
                    }
                }
            }

            foreach((int y, int x) item in snakeBody)
            {
                Map[item.y,item.x] = GameObjectType.GOT_Snake;
            }

            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    switch (Map[i, j])
                    {
                        case GameObjectType.GOT_Wall:
                            Console.Write("■");
                            break;
                        case GameObjectType.GOT_Space:
                            Console.Write("□");
                            break;
                        case GameObjectType.GOT_Snake:
                            Console.Write("●");
                            break;
                        case GameObjectType.GOT_Food:
                            Console.Write("★");
                            break;
                    }
                }
                Console.WriteLine();
            }
        }

        static void SetMapSize()
        {
            int newMapSize;
            Console.Write("새로운 MapSize 값을 입력하세요: ");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out newMapSize))
            {
                MapSize = newMapSize;
                GameMap = new GameObjectType[MapSize, MapSize];
            }
        }

        static void Main()
        {
            SetMapSize();

            Snake snake1 = new Snake();
            FoodCreater food1 = new FoodCreater();

            PrintMap(GameMap, snake1.SnakeBody, MapSize);

            ConsoleKey keyInfo = ConsoleKey.UpArrow;
            while (!isGameEnd)
            {
                //사용자의 key입력이 들어 왔을때
                if(Console.KeyAvailable)
                {
                    keyInfo = Console.ReadKey(true).Key;
                }
                snake1.Move(keyInfo);

                if (!isFoodExist)
                {
                    food1.RespawnFood(GameMap, snake1);
                    isFoodExist=true;
                }
                
                PrintMap(GameMap, snake1.SnakeBody, MapSize);
                Thread.Sleep(2000);
            }

            Console.WriteLine("Die");
        }

    }
}