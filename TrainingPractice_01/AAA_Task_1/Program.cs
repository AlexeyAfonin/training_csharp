using System;

namespace AAA_Task_1
{
    class Program
    {
        private const int _gemPrice = 10;
        static void Main(string[] args)
        {
            int gold;
            int gems = 0;
            int buygems;

balanceGold: 
            try
            {
                Console.WriteLine("Сколько у Вас золота?");
                gold = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Введены некоректные данные!\nВведите числовое значение!");
                goto balanceGold;
            }
buyGems:
            try
            {
                Console.WriteLine("Сколько хотите купить кристалов? (1 кристал = 10 золота)");
                buygems = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Введены некоректные данные!\nВведите числовое значение!");
                goto buyGems;
            }

            if (gold >= buygems * _gemPrice)
            {
                gold -= buygems * _gemPrice;
                gems = buygems;
                Console.WriteLine("Сделка успешно совершенна!");
            }
            else
                Console.WriteLine("\nНедостаточно золота");

            Console.WriteLine($"\nВаш баланс:\nЗолото: {gold}\nКристалы: {gems}");  
        }
    }
}
