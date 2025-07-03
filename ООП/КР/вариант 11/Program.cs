using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//  11     11       11
public interface IGraph
{
    int First();
}

public class Point : IComparable<Point>, IGraph
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Point(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Point operator +(Point p1, Point p2)
    {
        return new Point(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
    }

    public static Point operator -(Point p1, Point p2)
    {
        return new Point(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
    }

    public int CompareTo(Point other)
    {
        return (X + Y + Z).CompareTo(other.X + other.Y + other.Z);
    }

    public int First()
    {
        return (X > 0 && Y > 0) ? 1 : 0;
    }
}

public class Line : IGraph
{
    private Point p1;
    private Point p2;

    public Line(Point point1, Point point2)
    {
        p1 = point1;
        p2 = point2;
    }

    public int First()
    {
        return p1.First() + p2.First();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите первое число: ");
        string input1 = Console.ReadLine();
        Console.Write("Введите второе число: ");
        string input2 = Console.ReadLine();

        int number1 = int.Parse(input1);
        int number2 = int.Parse(input2);
        int sum = number1 + number2;

        string result = $"Сумма: {sum}";
        Console.WriteLine(result);

        string[,] stringArray = {
            { "hello", "world" },
            { "example", "text" },
            { "csharp", "programming" }
        };

        int totalLetters = 0;

        foreach (var str in stringArray)
        {
            totalLetters += str.Length;
        }

        Console.WriteLine($"Общее число букв в массиве: {totalLetters}");

        Point p1 = new Point(1, 2, 3);
        Point p2 = new Point(4, 5, 6);

        Point sumPoints = p1 + p2;
        Point differencePoints = p1 - p2;

        Console.WriteLine($"Сумма точек: ({sumPoints.X}, {sumPoints.Y}, {sumPoints.Z})");
        Console.WriteLine($"Разность точек: ({differencePoints.X}, {differencePoints.Y}, {differencePoints.Z})");

        if (p1.CompareTo(p2) < 0)
            Console.WriteLine("p1 меньше p2");
        else if (p1.CompareTo(p2) > 0)
            Console.WriteLine("p1 больше p2");
        else
            Console.WriteLine("p1 равно p2");

        Line line = new Line(p1, p2);
        Console.WriteLine($"Количество точек в первой четверти: {line.First()}");
    }
}