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
    public partial class Form3 : Form
    {
        public Form3(Form1 F1_)
        {
            InitializeComponent();
            try //Отображаем записи
            {
                textBox1.Text = File.ReadAllText(@"ScoreTable.txt");
            }
            catch //Если нет записей
            {
                textBox1.Text = "Нет записей игр";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"ScoreTable.txt", "");
            textBox1.Text = File.ReadAllText(@"ScoreTable.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
