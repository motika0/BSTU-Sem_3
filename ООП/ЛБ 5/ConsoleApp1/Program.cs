using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public enum AnimalType
    {
        Lion,
        Tiger,
        Owl,
        Parrot,
        Crocodile,
        Shark
    }

    public struct AnimalInfo
    {
        public AnimalType Type;
        public double Weight; 
        public int BirthYear; 

        public AnimalInfo(AnimalType type, double weight, int birthYear)
        {
            Type = type;
            Weight = weight;
            BirthYear = birthYear;
        }
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

        public object Clone()
        {
            return MemberwiseClone(); 
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

        public object Clone()
        {
            return MemberwiseClone(); 
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

        public object Clone()
        {
            return MemberwiseClone(); 
        }
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

        public object Clone()
        {
            return MemberwiseClone(); 
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

        public object Clone()
        {
            return MemberwiseClone(); 
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

        public object Clone()
        {
            return MemberwiseClone(); 
        }
    }

    public class Printer
    {
        public void IAmPrinting(Animal someObj)
        {
            Console.WriteLine(someObj.ToString());
            someObj.DoClone();
        }
    }


    public class AnimalContainer
    {
        private List<Animal> animals = new List<Animal>();

        public List<Animal> Animals
        {
            get { return animals; }
            set
            {
                if (value != null)
                {
                    animals = value;
                }
            }
        }

        public void AddAnimal(Animal animal)
        {
            if (animal != null)
            {
                animals.Add(animal);
            }
        }

        public void RemoveAnimal(Animal animal)
        {
            animals.Remove(animal);
        }

        public void PrintAnimals()
        {
            foreach (var animal in animals)
            {
                Console.WriteLine(animal.ToString());
            }
        }
    }

}

public class ZooController
{
    private AnimalContainer animalContainer;

    public ZooController()
    {
        animalContainer = new AnimalContainer();
    }

    public void AddAnimal(Animal animal)
    {
        animalContainer.AddAnimal(animal);
    }

    public double GetAverageWeight(AnimalType type)
    {
        var filteredAnimals = animalContainer.Animals.Where(a => (type == AnimalType.Lion && a is Lion) ||
                                                                 (type == AnimalType.Tiger && a is Tiger) ||
                                                                 (type == AnimalType.Owl && a is Owl) ||
                                                                 (type == AnimalType.Parrot && a is Parrot) ||
                                                                 (type == AnimalType.Crocodile && a is Crocodile) ||
                                                                 (type == AnimalType.Shark && a is Shark));

        return filteredAnimals.Any() ? filteredAnimals.Average(a => a.Weight) : 0;
    }

    public int CountPredatoryBirds()
    {
        return animalContainer.Animals.Count(a => a is Owl || a is Parrot);
    }

    public void PrintAnimalsSortedByBirthYear()
    {
        var sortedAnimals = animalContainer.Animals.OrderBy(a => a.BirthYear);
        foreach (var animal in sortedAnimals)
        {
            Console.WriteLine($"{animal.ToString()} - Год рождения: {animal.BirthYear}");
        }
    }

    public void ShowAverageWeight(AnimalType type)
    {
        double averageWeight = GetAverageWeight(type);
        Console.WriteLine($"Средний вес животных типа {type}: {averageWeight}");
    }

    public void ShowPredatoryBirdCount()
    {
        int count = CountPredatoryBirds();
        Console.WriteLine($"Количество хищных птиц: {count}");
    }

    public void ShowAnimalsSortedByBirthYear()
    {
        Console.WriteLine("Список животных, отсортированных по году рождения:");
        PrintAnimalsSortedByBirthYear();
    }
}



class Program
    {
        static void Main(string[] args)
        {
            ZooController zooController = new ZooController();

            Animal lion = new Lion { Weight = 190, BirthYear = 2015 };
            Animal tiger = new Tiger { Weight = 220, BirthYear = 2016 };
            Animal owl = new Owl { Weight = 1.5, BirthYear = 2018 };
            Animal parrot = new Parrot { Weight = 0.5, BirthYear = 2019 };
            Animal crocodile = new Crocodile { Weight = 300, BirthYear = 2014 };
            Animal shark = new Shark { Weight = 1500, BirthYear = 2017 };

            zooController.AddAnimal(lion);
            zooController.AddAnimal(tiger);
            zooController.AddAnimal(owl);
            zooController.AddAnimal(parrot);
            zooController.AddAnimal(crocodile);
            zooController.AddAnimal(shark);

            zooController.ShowAverageWeight(AnimalType.Lion);
            zooController.ShowPredatoryBirdCount();
            zooController.ShowAnimalsSortedByBirthYear();
        }
    }

