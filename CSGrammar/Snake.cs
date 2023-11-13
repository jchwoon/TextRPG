using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGrammar
{
    public class Snake
    {
        public Snake()
        {
            _snakeBody = new List<(int, int)>();
            _xPos = Program.MapSize / 2;
            _yPos = Program.MapSize / 2;

            _snakeBody.Add((_yPos, _xPos));
        }

        public int SnakePosX
        {
            get { return _xPos; }
            set { _xPos = value; }
        }

        public int SnakePosY
        {
            get{ return _yPos; }
            set{ _yPos = value; }
        }

        public List<(int, int)> SnakeBody
        {
            get { return _snakeBody; }
            set { _snakeBody = value; }
        }

        public void CheckGameOver()
        {
            if (_yPos == 0 || _xPos == 0 || _xPos == Program.MapSize - 1 || _yPos == Program.MapSize - 1)
            {
                Program.isGameEnd = true;
                return;
            }

            for (int i = 1; i < _snakeBody.Count; i++)
            {
                if (_snakeBody[i] == (_yPos, _xPos))
                {
                    Program.isGameEnd = true;
                    return;
                }
            }
        }

        public void Move(ConsoleKey keyInfo)
        {
            for (int i = _snakeBody.Count -1; i > 0; i--)
            {
                _snakeBody[i] = _snakeBody[i-1];
            }

            switch (keyInfo)
            {
                case ConsoleKey.UpArrow:
                    _yPos--;
                    break;
                case ConsoleKey.DownArrow:
                    _yPos++;
                    break;
                case ConsoleKey.LeftArrow:
                    _xPos--;
                    break;
                case ConsoleKey.RightArrow:
                    _xPos++;
                    break;
            }

            _snakeBody[0] = (_yPos, _xPos);

            CheckGameOver();

            if (Program.GameMap[_yPos, _xPos] == Program.GameObjectType.GOT_Food)
            {
                ConsumeFood();
            }
        }

        public void ConsumeFood()
        {
            Program.isFoodExist = false;
            _snakeBody.Add((_snakeBody[_snakeBody.Count -1]));
        }

        private int _xPos;
        private int _yPos;
        private List<(int, int)> _snakeBody;
    }
}
