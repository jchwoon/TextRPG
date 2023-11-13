using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGrammar
{
    public class FoodCreater
    {
        public FoodCreater() { }

        public bool CheckIsOkRespawnLocation(int randomY, int randomX, Snake snake)
        {
            bool isOk = true;

            
            for (int i = 0; i < snake.SnakeBody.Count; i++)
            {
                if (snake.SnakeBody[i].Item1 == randomY && snake.SnakeBody[i].Item2 == randomX)
                {
                    isOk = false;
                }
            }
            return isOk;
        }
        
        public void RespawnFood(Program.GameObjectType[,] Map, Snake snake)
        {
            
            Random random = new Random();

            int rowCount = Map.GetLength(0);
            int colCount = Map.GetLength(1);

            int randomX;
            int randomY;

            while(true)
            {
                randomX = random.Next(1, rowCount - 1);
                randomY = random.Next(1, colCount - 1);
         
                if (CheckIsOkRespawnLocation(randomY, randomX, snake))
                {
                    Map[randomY, randomX] = Program.GameObjectType.GOT_Food;
                    return;
                }
            }
        }
    }
}
