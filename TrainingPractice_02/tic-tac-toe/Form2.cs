using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace tic_tac_toe
{
    public partial class Form2 : Form
    {
        int[] _cells = { 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //Массив клеток нашего игрового поля. 0 - пустая, 1 - крестик, 2 - нолик

        //Массив победных комбинаций
        int[,] _winningCombinations =
        {
            //Горизонтальные
            /*0*/{ 0, 1, 2 }, 
            /*1*/{ 3, 4, 5 }, 
            /*2*/{ 6, 7, 8 }, 
            //Вертикальные
            /*3*/{ 0, 3, 6 }, 
            /*4*/{ 1, 4, 7 }, 
            /*5*/{ 2, 5, 8 }, 
            //Диагональные
            /*6*/{ 0, 4, 8 }, 
            /*7*/{ 2, 4, 6 } 
        };
        bool _onePlayer; //true - Игрок против Компьютера, false - Игрок против Игрока
        bool _movesEndend = false;
        bool _gameOver = false;
        /*Игрок против Компьютера*/
        bool _playerNotWin = true;
        /*Игрок против игрока*/
        char _whoMove = 'X';
        int _numMovePlayer = 1;
        //Запись игры в текстовый файл
        StreamWriter _scoreTable = new StreamWriter("ScoreTable.txt", true); //Получим доступ к существующему файлу, либо создадим новый

        public Form2(int howManyPlayers)
        {
            InitializeComponent();
            if (howManyPlayers == 1)
                _onePlayer = true;
            else
                _onePlayer = false;
        }
        //Отрабатываем нажатие кнопики
        private void ClickButton(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "") //Если объект типа "Кнопка" имеет пустой текст (пустая клекта)
            {
                if(_numMovePlayer == 1)
                    Controls["button" + Convert.ToInt32((sender as Button).Name[6].ToString())].ForeColor = Color.Red;
                else
                    Controls["button" + Convert.ToInt32((sender as Button).Name[6].ToString())].ForeColor = Color.Blue;
                
                Controls["button" + Convert.ToInt32((sender as Button).Name[6].ToString())].Text = _whoMove.ToString(); //Смотрим, какая кнопка(ячейка) была нажата и делаем в неё ход
                _cells[Convert.ToInt32((sender as Button).Name[6].ToString()) - 1] = _numMovePlayer;
                WinCheck(_whoMove);
                //Если игра не завершена и ещё есть куда ходить
                if ((!_movesEndend) && (!_gameOver))
                {
                    textBox1.Text =  $"Ход {_whoMove}";
                    //Проверяем, какой выбран редим (Один игрок(Игрок против Компьютера) или Два игрока)
                    if(_onePlayer) //Если один игрок
                        AIBot();
                    else //Если два игрока
                    {
                        //Определение, чей ход (X или O)
                        if (_whoMove == 'X')
                        {
                            _whoMove = 'O';
                            _numMovePlayer = 2;
                        }
                        else
                        {
                            _whoMove = 'X';
                            _numMovePlayer = 1;
                        }
                    }
                }
            }
        }
        //Искусственный интеллект Бота(Компьютера)
        private void AIBot()
        {
            _playerNotWin = true;
            for (int numCombination = 0; numCombination <= 7; numCombination++) //Перебераем все возможные комбинации
            {
                //Проверка, есть ли выигрышная комбинация для ноликов
                //Первый ряд
                if ((_cells[_winningCombinations[numCombination, 0]] == _cells[_winningCombinations[numCombination, 1]]) &&
                (_cells[_winningCombinations[numCombination, 1]] == 2) &&
                (_cells[_winningCombinations[numCombination, 2]] != 1))
                {
                    MoveBot(_winningCombinations[numCombination, 2]);
                    break;
                }
                //Второй ряд
                else if ((_cells[_winningCombinations[numCombination, 0]] == _cells[_winningCombinations[numCombination, 2]]) &&
                (_cells[_winningCombinations[numCombination, 2]] == 2) &&
                (_cells[_winningCombinations[numCombination, 1]] != 1))
                {
                    MoveBot(_winningCombinations[numCombination, 1]);
                    break;
                }
                //Третий ряд
                else if ((_cells[_winningCombinations[numCombination, 1]] == _cells[_winningCombinations[numCombination, 2]]) &&
                (_cells[_winningCombinations[numCombination, 2]] == 2) &&
                (_cells[_winningCombinations[numCombination, 0]] != 1))
                {
                    MoveBot(_winningCombinations[numCombination, 0]);
                    break;
                }
                else //Если выигрышной комбинации нет, то проверяем, выигрывает ли игрок
                {
                    if ((_cells[_winningCombinations[numCombination, 0]] == _cells[_winningCombinations[numCombination, 1]]) &&
                    (_cells[_winningCombinations[numCombination, 1]] == 1) &&
                    (_cells[_winningCombinations[numCombination, 2]] != 2))
                    {
                        MoveBot(_winningCombinations[numCombination, 2]);
                        break;
                    }
                    else if ((_cells[_winningCombinations[numCombination, 0]] == _cells[_winningCombinations[numCombination, 2]]) &&
                    (_cells[_winningCombinations[numCombination, 2]] == 1) &&
                    (_cells[_winningCombinations[numCombination, 1]] != 2))
                    {
                        MoveBot(_winningCombinations[numCombination, 1]);
                        break;
                    }
                    else if ((_cells[_winningCombinations[numCombination, 1]] == _cells[_winningCombinations[numCombination, 2]]) &&
                    (_cells[_winningCombinations[numCombination, 2]] == 1) &&
                    (_cells[_winningCombinations[numCombination, 0]] != 2))
                    {
                        MoveBot(_winningCombinations[numCombination, 0]);
                        break;
                    }
                }
            }
            //Если никаких комбинаций не найдено, ходим с случайную клетку
            if (_playerNotWin)
            {
                Random rand = new Random();
            move:
                int numCell = rand.Next(0, 8);
                if ((_cells[numCell] != 0) && (Controls["button" + (numCell + 1).ToString()].Text != ""))
                    goto move;
                MoveBot(numCell);
            }
            textBox1.Text = "ход X";
        }
        //Ход бота в указанную позицию(ячейку)
        private void MoveBot(int numCell)
        {
            _cells[numCell] = 2;
            Controls["button" + (numCell + 1).ToString()].ForeColor = Color.Blue;
            Controls["button" + (numCell + 1).ToString()].Text = "O";
            _playerNotWin = false;
            WinCheck('O');
        }
        //Проверка, выигрывает ли кто-нибудь или нет
        private void WinCheck(char whoseMove)
        {
            for (int numCombination = 0; numCombination <= 7; numCombination++) // Перебираем левый индекс массива (вариант комбинации)
            {
                /*Проверка массива _cells (игровое поле), для опредления победили ли мы или нет, 
                 * с помощью сравнения с одной из комбинаций указанных в _winningCombinations*/
                if (_cells[_winningCombinations[numCombination, 0]] == _cells[_winningCombinations[numCombination, 1]] &&
                    _cells[_winningCombinations[numCombination, 1]] == _cells[_winningCombinations[numCombination, 2]] &&
                    _cells[_winningCombinations[numCombination, 1]] != 0)
                {
                    if (whoseMove == 'X') MessageBox.Show("КРЕСТИКИ ПОБЕДИЛИ"); // сообщаем о победе крестиков
                    else if (whoseMove == 'O') MessageBox.Show("НОЛИКИ ПОБЕДИЛИ"); // сообщаем о победе ноликов

                    _movesEndend = false;
                    _gameOver = true;

                    WriteGame($"Победили {whoseMove}"); //Запишем результат игры в файл
                    break; //Выходим из цикла for
                }
                _movesEndend = true;
            }

            //Проверка, остались ли ещё возможные ходы
            int freeCells = 0;
            for(int i = 0; i < _cells.Length; i++)
                if (_cells[i] == 0) freeCells++;
            if (freeCells > 0) _movesEndend = false;

            if (_movesEndend) //Если закончились возможные ходы
            {
                _gameOver = true;
                MessageBox.Show("НИЧЬЯ");
                //Запишем результат игры в файл
                WriteGame("Ничья");
            }      
        }

        //Запись результата игры в текстовый файл
        private void WriteGame(string whoWin)
        {
            //Создаем новую запись игры и записываем её
            _scoreTable.WriteLine($"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}");
            _scoreTable.WriteLine(_onePlayer ? "Игрок(X) против Компьютера(O)" : "Игрок(X) против Игрока(O)");
            this.Text = _onePlayer ? "Один игрок" : "Два игрока";

            char[] temp = new char[_cells.Length];
            for (int i = 0; i < _cells.Length; i++)
            {
                if (_cells[i] == 1)
                    temp[i] = 'X';
                else if (_cells[i] == 2)
                    temp[i] = 'O';
                else if (_cells[i] == 0)
                    temp[i] = '*';
            }
            //Запишим игровое поле
            _scoreTable.WriteLine($"{temp[0]} {temp[1]} {temp[2]}");
            _scoreTable.WriteLine($"{temp[3]} {temp[4]} {temp[5]}");
            _scoreTable.WriteLine($"{temp[6]} {temp[7]} {temp[8]}");
            _scoreTable.WriteLine(whoWin); //Запишим итог игры
            _scoreTable.WriteLine("");
            _scoreTable.Close(); //Закрываем файл, чтобы сохранить данные и освободить процесс
        }

        //Отрабатывает при закрытии формы
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(_gameOver == false) //Если игра не была завершена
                WriteGame("Партия не завершена"); //Запишем результат игры в файл
        }

        //Перезапуск формы
        private void button10_Click(object sender, EventArgs e)
        {
            _gameOver = true;
            _scoreTable.Close();

            Form2 fr = new Form2(_onePlayer ? 1 : 2);
            fr.Show();

            this.Close();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
