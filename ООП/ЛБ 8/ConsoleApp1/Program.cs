using System;

public delegate void langevent(string message);

public class progalan
{
    public event langevent Renamed;
    public event langevent NewFeatureAdded;

    private string name;
    private string version;

    public string Name
    {
        get => name;
        set
        {
            name = value;
            Renamed?.Invoke($"Язык переименован в: {name}");
        }
    }

    public string Version
    {
        get => version;
        set
        {
            version = value;
            NewFeatureAdded?.Invoke($"Добавлена новая версия: {version}");
        }
    }

    public progalan(string name, string version)
    {
        Name = name;
        Version = version;
    }
}

public class StringProcessor
{
    public Action<string> ProcessString;

    public StringProcessor()
    {
        ProcessString += RemovePunctuation;
        ProcessString += AddSymbols;
        ProcessString += ToUpperCase;
        ProcessString += RemoveExtraSpaces;
        ProcessString += TrimString;
    }

    private void RemovePunctuation(string input)
    {
        var result = System.Text.RegularExpressions.Regex.Replace(input, @"[^\w\s]", "");
        Console.WriteLine($"Удаление знаков препинания: {result}");
    }

    private void AddSymbols(string input)
    {
        var result = "*" + input + "*";
        Console.WriteLine($"Добавление символов: {result}");
    }

    private void ToUpperCase(string input)
    {
        var result = input.ToUpper();
        Console.WriteLine($"Замена на заглавные: {result}");
    }

    private void RemoveExtraSpaces(string input)
    {
        var result = System.Text.RegularExpressions.Regex.Replace(input, @"\s+", " ");
        Console.WriteLine($"Удаление лишних пробелов: {result}");
    }

    private void TrimString(string input)
    {
        var result = input.Trim();
        Console.WriteLine($"Удаление пробелов в начале и конце: {result}");
    }
}

public class Program
{
    public static void Main()
    {
        var csharp = new progalan("C#", "9.0");
        var python = new progalan("Python", "3.9");

        csharp.Renamed += message => Console.WriteLine(message);
        csharp.NewFeatureAdded += message => Console.WriteLine(message);

        csharp.Name = "CSharp";
        csharp.Version = "10.0";

        python.Renamed += message => Console.WriteLine(message);
        python.NewFeatureAdded += message => Console.WriteLine(message);

        python.Name = "Python 3";
        python.Version = "3.10";

        var processor = new StringProcessor();
        string input = " Привет,  мир!   Как дела?  ";
        processor.ProcessString?.Invoke(input);
    }
}