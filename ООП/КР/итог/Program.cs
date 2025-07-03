using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using итог;

namespace итог
{

    public enum DayOfWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wendsday,
        Turhday,
        Friday,
        Saturday
    }

    public class ArrayOperetion
    {
        public static int Sum(int[] array)
        {
            int total = 0;
            foreach (var item in array)
            {
                total += item;
            }
            return total;
        }
    }
    public class Computer : IComparable<Computer>
    {
        public string Processor { get; set; }
        public int RAM { get; set; }
        public int Price { get; set; }


        public Computer(string processor, int ram, int price)
        {

            Processor = processor;
            RAM = ram;
            Price = price;

        }
        public int CompareTo(Computer other)
        {
            return this.RAM.CompareTo(other.RAM);
        }
    }

    class Programm
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Дни недели:");
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                Console.WriteLine(day);
            }

            int[] numbers = { 1, 2, 3, 4, 5 };
            int sum = ArrayOperetion.Sum(numbers);
            Console.WriteLine(sum);


            Computer comp1 = new Computer("ASUS", 8, 3120);
            Computer comp2 = new Computer("Mac", 16, 2760);

            Console.WriteLine($"Компьютер 1: {comp1.Processor}, RAM: {comp1.RAM} ГБ, Price: {comp1.Price} BYN");
            Console.WriteLine($"Компьютер 2: {comp2.Processor}, RAM: {comp2.RAM} ГБ, Price: {comp2.Price} BYN");

            if (comp1.CompareTo(comp2) > 0)
            {
                Console.WriteLine($"{comp1.Processor} имеет больше ram, чем {comp2.Processor}");
            }
            else if(comp1.CompareTo(comp2) < 0)

            {
                Console.WriteLine($"{comp2.Processor} имеет больше ram, чем {comp1.Processor}");
            }
             else
            {
                Console.WriteLine($"{comp2.Processor} и {comp1.Processor} равны по ram ");
            }

        }
    }
}
