using ConsoleApp1;
using System;
using static ConsoleApp1.Tiger;

namespace ConsoleApp1
{
    public interface ICloneable
    {
        bool DoClone();
    }

    public abstract class Animal
    {
        public abstract void zvuk();
        public abstract override string ToString();
        public abstract bool DoClone(); // Абстрактный метод
    }

    public class Mammal : Animal
    {
        public override void zvuk()
        {
            Console.WriteLine("Млекопитающее издает звук.");
        }

        public override string ToString()
        {
            return "Это млекопитающее.";
        }

        public override bool DoClone()
        {
            Console.WriteLine("Клонирование Млекопитающего через метод.");
            return true;
        }
    }

    public class Bird : Animal
    {
        public override void zvuk()
        {
            Console.WriteLine("Птица издает звук.");
        }

        public override string ToString()
        {
            return "Это птица.";
        }

        public override bool DoClone()
        {
            Console.WriteLine("Клонирование Птицы через метод.");
            return true;
        }
    }

    public class Fish : Animal
    {
        public override void zvuk()
        {
            Console.WriteLine("Рыба издает звук.");
        }

        public override string ToString()
        {
            return "Это рыба.";
        }

        public override bool DoClone()
        {
            Console.WriteLine("Клонирование Рыбы через метод.");
            return true;
        }
    }

    public sealed class Lion : Mammal, ICloneable
    {
        public override void zvuk()
        {
            Console.WriteLine("Рычит");
        }

        public override string ToString()
        {
            return "Это Лев.";
        }

        public override bool DoClone()
        {
            Console.WriteLine("Клонирование Льва через метод.");
            return true;
        }
    }

    public class Owl : Bird, ICloneable
    {
        public override void zvuk()
        {
            Console.WriteLine("Ухает");
        }

        public override string ToString()
        {
            return "Это Сова.";
        }

        public override bool DoClone()
        {
            Console.WriteLine("Клонирование Совы через метод.");
            return true;
        }
    }

    public class Tiger : Mammal, ICloneable
    {
        public override void zvuk()
        {
            Console.WriteLine("Рычит");
        }

        public override string ToString()
        {
            return "Это Тигр.";
        }

        public override bool DoClone()
        {
            Console.WriteLine("Клонирование Тигра через метод.");
            return true;
        }

        // Реализация интерфейса
        bool ICloneable.DoClone()
        {
            Console.WriteLine("Клонирование Тигра через интерфейс.");
            return true;
        }

        public class Crocodile : Fish, ICloneable
        {
            public override void zvuk()
            {
                Console.WriteLine("Шипит");
            }

            public override string ToString()
            {
                return "Это Крокодил.";
            }

            public override bool DoClone()
            {
                Console.WriteLine("Клонирование Крокодила через метод.");
                return true;
            }

        }

        public class Shark : Fish, ICloneable
        {
            public override void zvuk()
            {
                Console.WriteLine("Рычит в воде");
            }

            public override string ToString()
            {
                return "Это Акула.";
            }

            public override bool DoClone()
            {
                Console.WriteLine("Клонирование Акулы через метод.");
                return true;
            }
        }

        public class Parrot : Bird, ICloneable
        {
            public override void zvuk()
            {
                Console.WriteLine("Говорит");
            }

            public override string ToString()
            {
                return "Это Попугай.";
            }

            public override bool DoClone()
            {
                Console.WriteLine("Клонирование Попугая через метод.");
                return true;
            }
        }

        // Класс Printer с полиморфным методом
        public class Printer
        {
            public void IAmPrinting(Animal someObj)
            {
                Console.WriteLine(someObj.ToString());

                // Вызов метода DoClone() из Animal
                someObj.DoClone();
            }
        }


        class Program
        {
            static void Main(string[] args)
            {
                Animal[] animals = new Animal[]
                {
                new Lion(),
                new Owl(),
                new Tiger(),
                new Crocodile(),
                new Shark(),
                new Parrot()
                };

                Printer printer = new Printer();

                foreach (var animal in animals)
                {
                    printer.IAmPrinting(animal);
                    animal.zvuk();
                    Console.WriteLine();
                }

               
                Tiger tiger = new Tiger();
                Console.WriteLine("Тестирование класса Tiger:");
                tiger.zvuk();
                tiger.DoClone(); 
                ((ICloneable)tiger).DoClone(); // Вызов метода из интерфейса ICloneable
            }
        }
    }
}