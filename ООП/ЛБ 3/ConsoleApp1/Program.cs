using System;
using System.Collections.Generic;
using System.Linq;

public class Matrix
{
    private int[,] elements;
    private int rows;
    private int cols;

    // Вложенный класс Production
    public class Production
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }

        public Production(int id, string organizationName)
        {
            Id = id;
            OrganizationName = organizationName;
        }
    }

    // Вложенный класс Developer
    public class Developer
    {
        public string FullName { get; set; }
        public int Id { get; set; }
        public string Department { get; set; }

        public Developer(string fullName, int id, string department)
        {
            FullName = fullName;
            Id = id;
            Department = department;
        }
    }

    public Production Prod { get; set; }
    public Developer Dev { get; set; }

    public Matrix(int rows, int cols, Production production, Developer developer)
    {
        this.rows = rows;
        this.cols = cols;
        elements = new int[rows, cols];
        Prod = production;
        Dev = developer;
    }

    // Индексатор для доступа к элементам матрицы
    public int this[int row, int col]
    {
        get => elements[row, col];
        set => elements[row, col] = value;
    }

    // Свойства для получения количества строк и столбцов
    public int Rows => rows;
    public int Cols => cols;

    // Перегрузка оператора "+" для сложения матриц
    public static Matrix operator +(Matrix matrix1, Matrix matrix2)
    {
        if (matrix1.rows != matrix2.rows || matrix1.cols != matrix2.cols)
            throw new InvalidOperationException("Матрица должна быть того же размера.");

        Matrix result = new Matrix(matrix1.rows, matrix1.cols, matrix1.Prod, matrix1.Dev);
        for (int i = 0; i < matrix1.rows; i++)
        {
            for (int j = 0; j < matrix1.cols; j++)
            {
                result[i, j] = matrix1[i, j] + matrix2[i, j];
            }
        }
        return result;
    }

    // Перегрузка оператора "--" для обнуления всех элементов
    public static Matrix operator --(Matrix matrix)
    {
        for (int i = 0; i < matrix.rows; i++)
        {
            for (int j = 0; j < matrix.cols; j++)
            {
                matrix[i, j] = 0;
            }
        }
        return matrix;
    }

    // Перегрузка оператора "==" для сравнения матриц по нулевому столбцу
    public static bool operator ==(Matrix matrix1, Matrix matrix2)
    {
        return matrix1.GetZeroColumnCount() == matrix2.GetZeroColumnCount();
    }

    // Перегрузка оператора "!="
    public static bool operator !=(Matrix matrix1, Matrix matrix2)
    {
        return !(matrix1 == matrix2);
    }

    // Явное преобразование в int для подсчета отрицательных элементов
    public static explicit operator int(Matrix matrix)
    {
        int count = 0;
        for (int i = 0; i < matrix.rows; i++)
        {
            for (int j = 0; j < matrix.cols; j++)
            {
                if (matrix[i, j] < 0) count++;
            }
        }
        return count;
    }

    // Метод для получения количества нулевых столбцов
    private int GetZeroColumnCount()
    {
        int count = 0;
        for (int j = 0; j < cols; j++)
        {
            bool isZeroColumn = true;
            for (int i = 0; i < rows; i++)
            {
                if (elements[i, j] != 0)
                {
                    isZeroColumn = false;
                    break;
                }
            }
            if (isZeroColumn) count++;
        }
        return count;
    }

    // Метод для обнуления отрицательных элементов матрицы
    public void ZeroOutNegativeElements()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (elements[i, j] < 0)
                {
                    elements[i, j] = 0;
                }
            }
        }
    }

    // Переопределение ToString для удобного отображения матрицы
    public override string ToString()
    {
        var result = new List<string>();
        for (int i = 0; i < rows; i++)
        {
            var row = new List<string>();
            for (int j = 0; j < cols; j++)
            {
                row.Add(elements[i, j].ToString());
            }
            result.Add(string.Join(" ", row));
        }
        return string.Join("\n", result);
    }

    // Переопределение Equals и GetHashCode для корректного сравнения
    public override bool Equals(object obj)
    {
        if (obj is Matrix other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return GetZeroColumnCount();
    }
}

public static class StringExtensions
{
    // Метод для выделения первого числа в строке
    public static int? FirstNumberInString(this string input)
    {
        var words = input.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var word in words)
        {
            if (int.TryParse(word, out int num))
            {
                return num;
            }
        }
        return null; // Если числа нет, возвращаем null
    }
}

public static class StatisticOperation
{
    // Метод для получения суммы всех элементов матрицы
    public static int Sum(Matrix matrix)
    {
        int sum = 0;
        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Cols; j++)
            {
                sum += matrix[i, j];
            }
        }
        return sum;
    }

    // Метод для получения разницы между максимальным и минимальным элементом матрицы
    public static int DifferenceBetweenMaxMin(Matrix matrix)
    {
        int max = matrix[0, 0];
        int min = matrix[0, 0];
        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Cols; j++)
            {
                if (matrix[i, j] > max) max = matrix[i, j];
                if (matrix[i, j] < min) min = matrix[i, j];
            }
        }
        return max - min;
    }

    // Метод для подсчета количества элементов в матрице
    public static int CountElements(Matrix matrix)
    {
        return matrix.Rows * matrix.Cols;
    }
}

public class Program
{
    public static void Main()
    {
        var production = new Matrix.Production(1, "продукция");
        var developer = new Matrix.Developer("Ваня Иванов", 101, "отдел");

        Matrix matrix1 = new Matrix(2, 2, production, developer);
        matrix1[0, 0] = 1;
        matrix1[0, 1] = -1;
        matrix1[1, 0] = 3;
        matrix1[1, 1] = 4;

        Matrix matrix2 = new Matrix(2, 2, production, developer);
        matrix2[0, 0] = 2;
        matrix2[0, 1] = 2;
        matrix2[1, 0] = -3;
        matrix2[1, 1] = 5;

        // Сложение матриц
        Matrix sumMatrix = matrix1 + matrix2;
        Console.WriteLine("Сумма матриц:");
        Console.WriteLine(sumMatrix);

        // Обнуление всех элементов
        --matrix1;
        Console.WriteLine("\nМатрица после обнуления:");
        Console.WriteLine(matrix1);

        // Сравнение матриц по нулевому столбцу
        Console.WriteLine($"Матрицы равны по нулевому столбцу? {matrix1 == matrix2}");

        // Подсчет отрицательных элементов
        int negativeCount = (int)matrix1;
        Console.WriteLine($"\nКоличество отрицательных элементов в матрице: {negativeCount}");

        // Обнуление отрицательных элементов
        matrix1.ZeroOutNegativeElements();
        Console.WriteLine("\nМатрица после обнуления отрицательных элементов:");
        Console.WriteLine(matrix1);

        // Пример выделения первого числа в строке
        string exampleString = "Тест 5, 10, 15";
        int? firstNumber = exampleString.FirstNumberInString();
        Console.WriteLine($"Первое число в строке: {firstNumber}");

        // Использование методов StatisticOperation
        int sum = StatisticOperation.Sum(matrix2);
        Console.WriteLine($"\nСумма элементов второй матрицы: {sum}");

        int difference = StatisticOperation.DifferenceBetweenMaxMin(matrix2);
        Console.WriteLine($"Разница между максимальным и минимальным элементами второй матрицы: {difference}");

        int elementCount = StatisticOperation.CountElements(matrix2);
        Console.WriteLine($"Количество элементов во второй матрице: {elementCount}");
    }
}
/*public static implicit operator int(MyClass obj)
{
    return obj.Value; // Приведение MyClass к int
}

*/