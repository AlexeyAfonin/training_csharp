using System;

namespace AAA_Task_5
{
    class Program
    {
        private static int _player, _turnsBar = -1;
        private static char[] _maze;
        private static int _size = 0, _size2 = 0;
        private static bool _end = false;
        static void Main(string[] args)
        {
            const char ch_player = '■';
            const char ch_finish = 'X';
            const int turns = 100;

            ///Файл с лабиринтом должен быть расположен в bin\Debug\netcoreapp3.1 (В папке, где находится exe файл проекта)
            //Выводим лабиринт в консоль
            DrawMaze();
            //Информация
            Information(ch_player, ch_finish);

            // Ищем где находится игрок
            for (int i = 0; i < _maze.Length; i++)
                if (ch_player == _maze[i])
           _player = i;

            while (!_end)
            {
                int move;
                char key = Console.ReadKey(true).KeyChar;
                
                try
                {
                    if ((key == 'w') || (key == 'ц')) move = _player - 31;
                    else if ((key == 's') || (key == 'ы')) move = _player + 31;
                    else if ((key == 'a') || (key == 'ф')) move = _player - 1;
                    else if ((key == 'd') || (key == 'в')) move = _player + 1;
                    else move = -1;

                    if (_maze[move] == ' ')
                    {
                        TurnsBar(turns);
                        MovePlayer(_player, ' ');
                        MovePlayer(move, ch_player);
                        _player = move;
                    }
                    else if (_maze[move] == ch_finish)
                    {
                        TurnsBar(turns);
                        MovePlayer(_player, ' ');
                        MovePlayer(move, 'X');
                        Console.ForegroundColor = ConsoleColor.Green;
                        GameOver("ВЫ ПОБЕДИЛИ!");
                    }
                }
                catch
                {
                    continue;
                }
                
            }
            Console.ResetColor();
            Console.SetCursorPosition(35, 11);
            Console.Write($"Нажмите любую клавишу, чтобы продолжить...");
            Console.SetCursorPosition(35, 40);
            Console.ReadKey(true);
        }
        /// <summary>
        /// Выводим лабиринт из файла в консоль
        /// </summary>
        private static void Information(char player, char finish)
        {
            Console.SetCursorPosition(35, 0);
            Console.Write($"Ваш игрок: {player}. Цель игры: добраться до финиша {finish}.");
            Console.SetCursorPosition(35, 1);
            Console.Write($"Управление:");
            Console.SetCursorPosition(35, 2);
            Console.Write($"\tw|ц - Вверх");
            Console.SetCursorPosition(35, 3);
            Console.Write($"\ta|ф - Влев");
            Console.SetCursorPosition(35, 4);
            Console.Write($"\ts|ы - Вниз");
            Console.SetCursorPosition(35, 5);
            Console.Write($"\td|в - Вправо");
        }
        private static void DrawMaze()
        {
            //Читаем лабиринт из файла и записываем его
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"level.txt");
            while ((line = file.ReadLine()) != null)
                _size += line.Length;
            _maze = new char[_size];

            _size = 0;
            file = new System.IO.StreamReader(@"level.txt");
            //Выводим лабиринт в консоль
            while ((line = file.ReadLine()) != null)
            {
                char[] tempMaze = line.ToCharArray();
                Console.WriteLine(tempMaze);
                tempMaze.CopyTo(_maze, _size);
                _size += _size2 = line.Length;
            }
            file.Close();
        }
        /// <summary>
        /// Меняем позицию игрока
        /// </summary>
        public static void MovePlayer(int playerPosition, char player)
        {
            int top = playerPosition / _size2;
            int left = playerPosition - (top * _size2);
            Console.SetCursorPosition(left, top);
            Console.Write(player);
            _maze[playerPosition] = player;
        }
        /// <summary>
        /// Выводим сообщение о поражении/победе
        /// </summary>
        private static void GameOver(string massage)
        {
            Console.SetCursorPosition(35, 10);
            Console.WriteLine(massage);
            _end = true;
        }
        private static void TurnsBar(int turns)
        {
            _turnsBar++;
            int temp = 0;
            Console.SetCursorPosition(35, 6);
            Console.Write($"{_turnsBar} из {turns} ходов");
            Console.SetCursorPosition(35, 7);
            Console.Write("[");
            for (int i = 0; i <= turns; i++) if (i % 5 == 0)
            {
                temp++;
                Console.SetCursorPosition(35 + temp, 7);
                if(i <= _turnsBar)
                    Console.Write('#');
                else
                    Console.Write('_');
            }
            Console.Write("]");

            if (_turnsBar >= turns)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                GameOver("Вы проиграли! Не осталось ходов!");
            }
        }
    }
}
