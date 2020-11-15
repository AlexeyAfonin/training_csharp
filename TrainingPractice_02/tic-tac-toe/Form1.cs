using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tic_tac_toe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Запуск игры с режимом Игрок против Компьютера (Один игрок)
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 startGame_OnePlayer = new Form2(1);
            startGame_OnePlayer.Show();
        }
        //Запуск игры с режимом Игрок против Игрока (Два игрока)
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 startGame_TwoPlayer = new Form2(2);
            startGame_TwoPlayer.Show();
        }
        //Открываем окно с записью игр
        private void button3_Click(object sender, EventArgs e)
        {
            Form3 recordGames = new Form3(this);
            recordGames.Show();
        }
        //Выход из приложения
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
