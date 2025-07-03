using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public static class Reflector
{
    public static string GetAssemblyName(string className)
    {
        Type type = Type.GetType(className);
        return type?.Assembly.FullName ?? "Класс не найден";
    }
    public static bool HasPublicConstructors(string className)
    {
        Type type = Type.GetType(className);
        return type?.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Any() ?? false;
    }
    public static IEnumerable<string> GetPublicMethods(string className)
    {
        Type type = Type.GetType(className);
        return type?.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                    .Select(m => m.Name) ?? Enumerable.Empty<string>();
    }

    public static IEnumerable<string> GetFieldsAndProperties(string className)
    {
        Type type = Type.GetType(className);
        var fields = type?.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                          .Select(f => $"поля: {f.Name}");
        var properties = type?.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                             .Select(p => $"Свойство: {p.Name}");
        return fields?.Concat(properties) ?? Enumerable.Empty<string>();
    }

    public static IEnumerable<string> GetImplementedInterfaces(string className)
    {
        Type type = Type.GetType(className);
        return type?.GetInterfaces().Select(i => i.Name) ?? Enumerable.Empty<string>();
    }

    public static IEnumerable<string> GetMethodsWithParameterType(string className, string parameterTypeName)
    {
        Type type = Type.GetType(className);
        Type parameterType = Type.GetType(parameterTypeName);
        return type?.GetMethods()
                    .Where(m => m.GetParameters().Any(p => p.ParameterType == parameterType))
                    .Select(m => m.Name) ?? Enumerable.Empty<string>();
    }

    public static void InvokeMethod(object obj, string methodName, string parametersFilePath)
    {
        MethodInfo method = obj.GetType().GetMethod(methodName);
        if (method == null) throw new ArgumentException("Метод не найден");

        ParameterInfo[] parameters = method.GetParameters();
        object[] parameterValues = new object[parameters.Length];

        using (StreamReader reader = new StreamReader(parametersFilePath))
        {
            string line;
            int index = 0;
            while ((line = reader.ReadLine()) != null && index < parameters.Length)
            {
                parameterValues[index] = Convert.ChangeType(line, parameters[index].ParameterType);
                index++;
            }
        }

        method.Invoke(obj, parameterValues);
    }

    public static T Create<T>() where T : class
    {
        Type type = typeof(T);
        ConstructorInfo constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();

        if (constructor != null)
        {
            return (T)constructor.Invoke(null);
        }

        throw new InvalidOperationException("Нет публичного конструктора для данного типа");
    }

    public static void WriteToFile(string filePath, string className)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"Класс: {className}");
            writer.WriteLine($"Асемблай: {GetAssemblyName(className)}");
            writer.WriteLine($"Имеет публичный конструктор: {HasPublicConstructors(className)}");
            writer.WriteLine($"Публичные метода: {string.Join(", ", GetPublicMethods(className))}");
            writer.WriteLine($"Части и свойства: {string.Join(", ", GetFieldsAndProperties(className))}");
            writer.WriteLine($"Интерфейсы: {string.Join(", ", GetImplementedInterfaces(className))}");
            writer.WriteLine();
        }
    }
}


public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person() { }
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Greet()
    {
        Console.WriteLine($"Я {Name}, и мне {Age} лет.");
    }
}

public class Car
{
    public string Model { get; set; }
    public int Year { get; set; }

    public Car() { }
    public Car(string model, int year)
    {
        Model = model;
        Year = year;
    }

    public void Drive()
    {
        Console.WriteLine($"{Model} едет.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = "reflector_output.txt";

        string personClass = "Человек";
        Reflector.WriteToFile(filePath, personClass);

        Person person = Reflector.Create<Person>();
        person.Name = "Вася";
        person.Age = 30;
        person.Greet();

        string carClass = "Машина";
        Reflector.WriteToFile(filePath, carClass);

        Car car = Reflector.Create<Car>();
        car.Model = "Toyota";
        car.Year = 2020;
        car.Drive();

        Console.WriteLine(File.ReadAllText(filePath));
    }
}