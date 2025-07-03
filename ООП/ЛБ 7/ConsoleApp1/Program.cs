using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public interface allinterface<T>
{
    void Add(T item);
    void Remove(T item);
    IEnumerable<T> ViewAll();
}

public class CollectionType<T> : allinterface<T> where T : class
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        try
        {
            items.Add(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция добавления завершена.");
        }
    }

    public void Remove(T item)
    {
        try
        {
            if (!items.Remove(item))
            {
                Console.WriteLine("Элемент не найден.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при удалении: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Операция удаления завершена.");
        }
    }

    public IEnumerable<T> ViewAll()
    {
        return items.AsEnumerable();
    }

    public void SF(string filePath)
    {
        try
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var item in items)
                {
                    writer.WriteLine(item.ToString());
                }
            }
            Console.WriteLine("Данные успешно сохранены в файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении в файл: {ex.Message}");
        }
    }

    public void LF(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                items.Clear();
                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Здесь предполагается, что T имеет конструктор, принимающий строку
                        // или можно использовать фабричный метод для создания объекта
                        // Пример: items.Add(new T(line)); (нужна реализация)
                    }
                }
                Console.WriteLine("Данные успешно загружены из файла.");
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при загрузке из файла: {ex.Message}");
        }
    }
}

public abstract class Animal
{
    public abstract void zvuk();
    public abstract override string ToString();
    public abstract bool DoClone(); 
}

public class Tiger : Animal
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
}

class Program
{
    static void Main(string[] args)
    {
        var tigerCollection = new CollectionType<Tiger>();
        tigerCollection.Add(new Tiger());
        tigerCollection.Add(new Tiger());

        Console.WriteLine("Тигры в коллекции:");
        foreach (var tiger in tigerCollection.ViewAll())
        {
            Console.WriteLine(tiger);
        }

        tigerCollection.SF("tigers.txt");

        tigerCollection.LF("tigers.txt");

        Console.WriteLine("Тигры после загрузки из файла:");
        foreach (var tiger in tigerCollection.ViewAll())
        {
            Console.WriteLine(tiger);
        }


    }
}