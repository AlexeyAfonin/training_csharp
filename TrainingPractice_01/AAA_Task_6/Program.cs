using System;

namespace AAA_Task_6
{
    class Program
    {
        private static string[,] _dossier = new string[0, 2]; // Досье[НомерДосье, ФИО и Должность]
        private static int _size = 0; //Размер списка досье

        static void Main(string[] args)
        {
            while (true)
            {
            numberCommand:
                Console.Clear();
                Console.WriteLine("Отдел кадров:" +
                    "\n\t1. Добавить досье " +
                    "\n\t2. Вывести все досье " +
                    "\n\t3. Удалить досье " +
                    "\n\t4. Поиск по фамилии " +
                    "\n\t5. Выйти из программы");  
                Console.WriteLine("\nВведите номер команды:");
                char numberCommand = Console.ReadKey(true).KeyChar;
                if (numberCommand == '1') //Добавить досье
                {
                surname:
                    Console.WriteLine("Введите фамилию: ");
                    string surname = Console.ReadLine();
                    if (surname.Length < 3)
                    {
                        Console.WriteLine("Фамилия должна содержать больше 3х букв!");
                        goto surname;
                    }
                name:
                    Console.WriteLine("Введите имя: ");
                    string name = Console.ReadLine();
                    if (name.Length < 3)
                    {
                        Console.WriteLine("Имя должно содержать больше 3х букв!");
                        goto name;
                    }
                    Console.WriteLine("Введите отчество: ");
                    string patronymic = Console.ReadLine();
                    Console.WriteLine("Введите должность: ");
                    string post = Console.ReadLine();

                    string fio = surname + " " + name + " " + patronymic;
                    AddDossier(fio, post);
                }
                else if (numberCommand == '2') //Вывести все досье
                {
                    List();
                }
                else if (numberCommand == '3') //Удалить досье
                {
                    if (_size < 1)
                    {
                        Console.WriteLine("В базе нет досье.");
                        Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine($"Список досье:");
                        for (int i = 0; i < _size; i++)
                            Console.WriteLine($"{i + 1}) {_dossier[i, 0]} - {_dossier[i, 1]}");
                    numberDosie: 
                        try
                        {
                            Console.WriteLine($"Введите номер для удаления досье");
                            int numberDossier = Convert.ToInt32(Console.ReadLine());
                            RemoveDossier(numberDossier - 1);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Некорректный номер досье!");
                            goto numberDosie;
                        }
                    }
                }
                else if (numberCommand == '4') //Найти досье по фамилии
                {
                    Console.WriteLine("Введите фамилию для поиска: ");
                    string surname = Console.ReadLine();
                    SearchDossier(surname);
                }
                else if (numberCommand == '5') //Выйти из программы
                {
                    Console.WriteLine("Завершение работы программы..");
                    Environment.Exit(1);
                }
                else //Если такого номера комманды нет
                {
                    Console.WriteLine("Комманды под таким номер не существует! Выберете комманду от 1 до 5");
                    Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить");
                    Console.ReadKey();
                    goto numberCommand;
                }
            }
        }

        private static void AddDossier(string surname, string post)
        {
            NewSize(_size + 1);
            _dossier[_size - 1, 0] = surname;
            _dossier[_size - 1, 1] = post;

            Console.WriteLine($"Добавленно досье: " +
                $"\n\tФИО: {surname} " +
                $"\n\tДолжность: {post}" +
                $"\n\tНомер досье: {_size}");
            Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить");
            Console.ReadKey();
        }
        private static void NewSize(int newsize)
        {
            if(newsize < 0)
            {
                Console.WriteLine($"ERROR: Неправильный размер массива!");
                Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey();
                return;
            }
            if (newsize != _size)
            {
                string[,] tempDossier = new string[newsize, 2];
                for (int i = 0; i < Math.Min(_size, newsize); i++)
                {
                    tempDossier[i, 0] = _dossier[i, 0];
                    tempDossier[i, 1] = _dossier[i, 1];
                }
                if (newsize > _size) 
                    for (int i = _size; i < newsize; i++)
                    {
                        tempDossier[i, 0] = "";
                        tempDossier[i, 1] = "";
                    }
                _dossier = tempDossier;
                _size = newsize;
            }
        }
        private static void List()
        {
            if (_size == 0)
                Console.WriteLine("Список досье пустой!");
            else
            {
                Console.WriteLine($"Список досье:");
                for (int i = 0; i < _size; i++)
                    Console.WriteLine($"{i + 1}) {_dossier[i, 0]} - {_dossier[i, 1]}");
            }
            Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить");
            Console.ReadKey();
        }
        private static void RemoveDossier(int numberDossier)
        {
            if (numberDossier >= _size || numberDossier < 0 || _size <= 0)
            {
                Console.WriteLine($"Неправильный индекс. Повторите попытку!");
                Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey();
                return;
            }

            string[,] tempDossier = new string[_size - 1, 2]; //Временная переменная для обновления списка(массива)
            int tempNum = 0;
            for (int i = 0; i < _size; i++)
            {
                if (i != numberDossier)
                {
                    tempDossier[tempNum, 0] = _dossier[i, 0];
                    tempDossier[tempNum, 1] = _dossier[i, 1];
                    tempNum++;
                }
                else
                    Console.WriteLine($"Досье {_dossier[i, 0]} успешно удалено!");
            }
            _dossier = tempDossier;
            _size -= 1;

            Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить");
            Console.ReadKey();
        }

        private static void SearchDossier(string str)
        {
            int count = 0;
            for (int i = 0; i < _size; i++)
            {
                if (_dossier[i, 0].IndexOf(str, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine($"Досье: {i + 1}) {_dossier[i, 0]} - {_dossier[i, 1]}");
                    count++;
                }
            }
            if (count == 0) 
                Console.WriteLine("Досье с такой фамилий не найдено!");
            Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить");
            Console.ReadKey();
        }
    }
}