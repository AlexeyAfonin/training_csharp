using System;

namespace AAA_Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string text;
            Console.WriteLine("Введите текст: \n |Чтобы закончить ввыод - Введите exit|");

            while (true) //Запуск бесконечного цикла
            {
                text = Console.ReadLine();
                if (text == "exit")
                    break; //Выход из цикла
            }

        }
    }
}
