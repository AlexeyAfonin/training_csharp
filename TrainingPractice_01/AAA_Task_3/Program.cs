using System;

namespace AAA_Task_3
{
    class Program
    {
        private const string _password = "IP183AAA";
        private const string _message = "Привет!";
        static void Main(string[] args)
        {
            int attempt = 0;
            string pass = "";
            while (attempt <= 3)
            {
                attempt++;
                if (attempt > 3)
                {
                    Console.WriteLine("Доступ заблокирован!\nНажмите любую клавишу, чтобы завершить работу программы:...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"Введите пароль. Попытка {attempt} из 3");
                    pass = Console.ReadLine();
                    if (pass == _password)
                        Console.WriteLine($"Секретное сообщение: {_message}");
                }
            }
        }
    }
}
