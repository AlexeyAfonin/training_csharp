using System;

namespace AAA_Task_5
{
    class Program
    {
        private static int _playerPos;
        private static char[,] _maze = new char[1000, 1000];
        private static char[] _lenghtMaze; 
        private static int height = 0, width = 0;
        private static int _posCursor = 1;
        static void Main(string[] args)
        {
            const char ch_player = '■';
            const char ch_enemy = '*';
            const char ch_finish = 'X';

            //Читаем лабиринт из файла и записываем его
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"level.txt");
            _lenghtMaze = new char[1000];
            int size = 0;
            while ((line = file.ReadLine()) != null)
            {
                foreach (char ch in line)
                {
                    _maze[height, width] = ch;
                    _lenghtMaze[size] = ch;
                    size++;
                    width++;
                }
                height++;    
            }

            //Выводим лабиринт в консоль
            for (int i = 0; i < height; i++) 
                for (int j = 0; j < width; j++)
                    Console.Write(_maze[i, j]);


            //Инструкция
            Console.SetCursorPosition(35, 0);
            Console.Write($"Вы - {ch_player}. Цель игры - попасть в точку {ch_finish},");
            Console.SetCursorPosition(35, 1);
            Console.Write($"не встретившись с врагом {ch_enemy}!");
            Console.SetCursorPosition(35, 2);

            // Ищем где находится игрок
            _playerPos = GetChar(ch_player);

            Console.SetCursorPosition(_playerPos, _posCursor);
            while (true)
            {
                char key = Console.ReadKey(true).KeyChar;
                Console.SetCursorPosition(_playerPos - 31, _posCursor);
                Console.Write(' ');

                if (key == 'w')
                {
                   _posCursor -= 1;
                }
                else if (key == 's')
                {
                    _posCursor += 1;
                }
                else if (key == 'a')
                {
                    _playerPos -= 1;
                }
                else if (key == 'd')
                {
                    _playerPos += 1;
                }
                else
                {
                    _posCursor = _posCursor + 1 - 1;
                    _playerPos = _playerPos + 1 - 1;
                }

                MovePlayer(ch_player, _playerPos - 31, _posCursor);         
            }
        }
        private static int GetChar(char ch)//Получаем символ
        {
            for (int i = 0; i < _lenghtMaze.Length; i++)
                if (ch == _lenghtMaze[i]) return i;
            return 0;
        }
        private static void MovePlayer(char player, int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine(player);
        }
        private static void MoveEnemies()
        {

        }
        private static void GameOver()
        {

        }
    }
}
