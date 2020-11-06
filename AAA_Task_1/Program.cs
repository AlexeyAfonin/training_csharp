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

            Console.WriteLine("Сколько у Вас золота?");
            gold = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Сколько хотите купить кристалов? (1 кристал = 10 золота)");
            int buygems = Convert.ToInt32(Console.ReadLine());

            if (gold >= buygems *  _gemPrice)
            {
                gold -= buygems * _gemPrice;
                gems = buygems;
            }
            else
            {
                Console.WriteLine("\nНедостаточно золота");
            }

            Console.WriteLine($"\nВаш баланс:\nЗолото: {gold}\nКристалы: {gems}");
        }
    }
}
