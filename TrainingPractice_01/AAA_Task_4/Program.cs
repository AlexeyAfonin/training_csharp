using System;
using System.Collections.Generic;

namespace AAA_Task_4
{
    class Program
    {
        static void Main(string[] args)
        {
            int hpPlayer = Player.hp;
            int hpBoss = Boss.hp;
            int moveNumber = 0;
            string[] spellNames = new string[5] 
            {   
                "Заклинание Рашамон - 1", 
                "Заклинание Хуганзакура - 2", 
                "Заклинание Межпространственный разлом - 3", 
                "Огненный шар - 4", 
                "Магический щит - 5"
            };
            int numberSpell;
            bool shadowSpiritSummoned = false;
            bool fireballIsUsing = false;
            bool playerIsDefended = false;

            Console.ForegroundColor = ConsoleColor.Cyan; //Поменять цвет текста
            Console.WriteLine("***Схватка с боссом***");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Здоровье Игрока: {hpPlayer} | Здоровье Босса {hpBoss}");

            while (true)
            {
                moveNumber++;
                if(moveNumber % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green; //Поменять цвет текста на зеленый
                    Console.WriteLine("-----Ход игрока-----");
    selectionSpells:Console.ForegroundColor = ConsoleColor.Yellow; //Поменять цвет текста на желтый
                    Console.WriteLine("Выберите номер заклинания\n" +
                        "\tЗаклинание Рашамон - 1,\n" +
                        "\tЗаклинание Хуганзакура - 2,\n" +
                        "\tЗаклинание Межпространственный разлом - 3\n" +
                        "\tОгненный шар - 4\n" +
                        "\tМагический щит - 5");
                    try 
                    {
                        numberSpell = Convert.ToInt32(Console.ReadLine());
                        if((numberSpell > 5)||(numberSpell < 1))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Такого заклинания нет!");
                            goto selectionSpells;
                        }
                        switch (numberSpell)
                        {
                            case 1:
                                if (shadowSpiritSummoned == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("*Теневой дух призван");
                                    shadowSpiritSummoned = Player.RashamonSpellAttack();
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("*Нельзя призвать больше 1 теневого духа");
                                    goto selectionSpells;
                                }
                                break;
                            case 2:
                                if (shadowSpiritSummoned == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    hpBoss -= Player.HuganzakuraSpellAttack(100);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("*Для начала призовите теневого духа (Рашамон)");
                                    goto selectionSpells;
                                }
                                break;
                            case 3:
                                Console.ForegroundColor = ConsoleColor.Green;
                                hpPlayer += Player.InterdimensionalRiftSpellAttack(250);
                                break;
                            case 4:
                                if (fireballIsUsing == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    hpBoss -= Player.FireballAttack(300);
                                    fireballIsUsing = true;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("*Огненный шар можно использовать лишь один раз за бой!");
                                    goto selectionSpells;
                                }
                                break;
                            case 5:
                                playerIsDefended = true;
                                break;
                        }
                        if (hpBoss <= 0) 
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("***БОСС ПОБЕЖДЕН***");
                            break;
                        }
                    }
                    catch(FormatException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Такого заклинания нет!");
                        goto selectionSpells;
                    }
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("-----Ход босса-----");
                    Console.ResetColor();
                    int damage = Boss.RashamonSpellAttack(100);
                    if (playerIsDefended == false)
                    {
                        hpPlayer -= damage;
                        Console.Write($"*Нанесенный урон Игроку ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(damage);
                    }
                    else
                    {
                        hpPlayer -= damage / 2;
                        Console.Write($"*Нанесенный урон Игроку ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(damage);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("| Заблокировано ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{damage / 2} урона");
                        playerIsDefended = false;
                    }
                    if(hpPlayer <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("***ВЫ ПРОИГРАЛИ***");
                        break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Здоровье Игрока: {hpPlayer} | Здоровье Босса {hpBoss}");
                Console.ResetColor();
            }
        }
    }
    class Boss
    {
        public const int hp = 500;
        public static int RashamonSpellAttack(int dmg)
        {
            dmg = new Random().Next(dmg, dmg * 4);
            return dmg;
        }
    }
    class Player
    {
        public const int hp = 1000;

        public static bool RashamonSpellAttack()
        {
            return true;
        }
        public static int HuganzakuraSpellAttack(int dmg)
        {
            Console.Write("*Нанесенный урон Боссу ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(dmg);
            return dmg;
        }
        public static int InterdimensionalRiftSpellAttack(int regen)
        {
            Console.Write("*Восстановленно ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{regen} HP");
            return regen;
        } 
        public static int FireballAttack(int dmg)
        {
            Console.Write("*Нанесенный урон Боссу ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(dmg);
            return dmg;
        }
        public void MagicShieldSpell()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Магический щит призван!");
        }
    }
}
