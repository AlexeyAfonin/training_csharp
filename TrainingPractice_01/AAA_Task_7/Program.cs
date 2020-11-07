using System;

namespace AAA_Task_7
{
    class Program
    {
        static void Main(string[] args)
        {
            int size;
            Random rand = new Random();
            int[] array;

        enterSize:
            try
            {
                Console.Write("Введите размер массива: ");
                size = Convert.ToInt32(Console.ReadLine());
                if (size <= 0)
                {
                    Console.WriteLine("Размер массива не может быть меньшне или равнен нулю.");
                    goto enterSize;
                }
                else
                {
                    array = new int[size];
                    for (int i = 0; i < size; i++)
                        array[i] = rand.Next(0, 100);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Введите числовое значение!");
                goto enterSize;
            }
            
            Console.WriteLine("\nПервычный массив:");
            for (int i = 0; i < size; i++) 
                Console.Write(array[i] + " ");

            //Перемешиваем массив
            for (int i = 0; i < size; i++)
            {
                int randomSelect = rand.Next(0, i + 1);
                int tempArray = array[randomSelect];

                array[randomSelect] = array[i];
                array[i] = tempArray;
            }

            Console.WriteLine("\n\nПеремешанный массив:");
            for (int i = 0; i < size; i++) 
                Console.Write(array[i] + " ");

            Console.WriteLine("\nНажминте любую клавишу, для выхода из программы...");
            Console.ReadKey();
        }
    }
}
