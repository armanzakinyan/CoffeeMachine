using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetCursorPosition(Console.WindowWidth / 3, 0);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("----Welcome To The Coffee House----");
            Console.ForegroundColor = ConsoleColor.Green;
            CoffeeMachine machine = new CoffeeMachine();
            machine.PrintProducts();
            Console.WriteLine();
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                link1:
                Console.Write("Insert Your Coin => ");
                bool successMoney = false;
                while (!successMoney)
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        machine.Money = int.Parse(Console.ReadLine());
                        successMoney = true;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Insert Correct Coin! "+" => ");
                    }
                }
                link2:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter Number Of Coffee From 1 To 10 => ");
                bool successCoffeeNum = false;
                while (!successCoffeeNum)
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        machine.CoffeeNum = int.Parse(Console.ReadLine());
                        successCoffeeNum = true;
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("Enter Correct Number Of Coffee " + " => ");
                    }
                }
                Coffee usersCoffee = new Coffee();
                try
                {
                    usersCoffee = machine.MakeCoffee();
                }
                catch (IngridientException e)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Please Select Another Coffee");
                    goto link2;
                }
                catch (MoneyException eMoney)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(eMoney.Message);
                    machine.Money = 0;
                    Console.WriteLine("Take Your Money Back And Try Again");
                    goto link1;

                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Your " + usersCoffee.Name + " Is Ready!");
                link3:
                Console.Write("Enter '0' To Finish...");
                ConsoleKeyInfo info = Console.ReadKey();
                if (info.Key == ConsoleKey.D0 || info.Key == ConsoleKey.NumPad0)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    int cashBack = machine.Money - usersCoffee.Price;
                    if (cashBack == 0)
                        Console.WriteLine("You Don't Have Change...");
                    else
                        Console.WriteLine("Your Change Is " + cashBack);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("If You Want Another Coffee Press Enter");
                }
                else
                {
                    Console.WriteLine();
                    goto link3;
                }
                machine.SaveChangesInFile();
                Console.WriteLine();
                Console.WriteLine();
                machine.Money = 0;
            }
            while (Console.ReadKey().Key == ConsoleKey.Enter);
        }
    }
}
