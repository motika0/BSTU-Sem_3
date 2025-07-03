using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;

public interface ISerializer
{
    void Serialize<T>(T obj, string filePath);
    T Deserialize<T>(string filePath);
}

public class JsonSerializer : ISerializer
{
    public void Serialize<T>(T obj, string filePath)
    {
        string json = System.Text.Json.JsonSerializer.Serialize(obj);
        File.WriteAllText(filePath, json);
    }

    public T Deserialize<T>(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return System.Text.Json.JsonSerializer.Deserialize<T>(json);
    }
}

public class XmlSerializerWrapper : ISerializer
{
    public void Serialize<T>(T obj, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, obj);
        }
    }

    public T Deserialize<T>(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (StreamReader reader = new StreamReader(filePath))
        {
            return (T)serializer.Deserialize(reader);
        }
    }
}

public class BinarySerializer : ISerializer
{
    public void Serialize<T>(T obj, string filePath)
    {
        IFormatter formatter = new BinaryFormatter();
        using (Stream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            formatter.Serialize(stream, obj);
        }
    }

    public T Deserialize<T>(string filePath)
    {
        IFormatter formatter = new BinaryFormatter();
        using (Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            return (T)formatter.Deserialize(stream);
        }
    }
}

[Serializable]
public class Tiger : Animal, ICloneable
{
    [NonSerialized]
    private string secret = "This is a secret";

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

public abstract class Animal
{
    public abstract void zvuk();
    public abstract bool DoClone();
    public override abstract string ToString();
}

public class Lion : Animal
{
    public override void zvuk() { Console.WriteLine("Рычит как Лев"); }
    public override string ToString() { return "Это Лев."; }
    public override bool DoClone() { return true; }
}

public class Owl : Animal
{
    public override void zvuk() { Console.WriteLine("Ухает"); }
    public override string ToString() { return "Это Сова."; }
    public override bool DoClone() { return true; }
}

public class Program
{
    public static void Main(string[] args)
    {
        Animal[] animals = new Animal[]
        {
            new Lion(),
            new Owl(),
            new Tiger()
        };

        ISerializer jsonSerializer = new JsonSerializer();
        jsonSerializer.Serialize(animals, "animals.json");
        Animal[] loadedJsonAnimals = jsonSerializer.Deserialize<Animal[]>("animals.json");
        Console.WriteLine("JSON:");
        foreach (var animal in loadedJsonAnimals)
        {
            Console.WriteLine(animal);
        }

        ISerializer xmlSerializer = new XmlSerializerWrapper();
        xmlSerializer.Serialize(animals, "animals.xml");
        Animal[] loadedXmlAnimals = xmlSerializer.Deserialize<Animal[]>("animals.xml");
        Console.WriteLine("XML:");
        foreach (var animal in loadedXmlAnimals)
        {
            Console.WriteLine(animal);
        }

        ISerializer binarySerializer = new BinarySerializer();
        binarySerializer.Serialize(animals, "animals.bin");
        Animal[] loadedBinaryAnimals = binarySerializer.Deserialize<Animal[]>("animals.bin");
        Console.WriteLine("Binary:");
        foreach (var animal in loadedBinaryAnimals)
        {
            Console.WriteLine(animal);
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("animals.xml");
        XmlNodeList lionNodes = xmlDoc.SelectNodes("//Lion");
        XmlNodeList owlNodes = xmlDoc.SelectNodes("//Owl");
        Console.WriteLine("XPath:");
        foreach (XmlNode node in lionNodes)
        {
            Console.WriteLine(node.OuterXml);
        }
        foreach (XmlNode node in owlNodes)
        {
            Console.WriteLine(node.OuterXml);
        }

        XElement xml = XElement.Load("animals.xml");
        var lions = from el in xml.Descendants("Lion")
                    select el;
        var owls = from el in xml.Descendants("Owl")
                   select el;
        Console.WriteLine("Linq to XML:");
        foreach (var lion in lions)
        {
            Console.WriteLine(lion);
        }
        foreach (var owl in owls)
        {
            Console.WriteLine(owl);
        }
    }
}
