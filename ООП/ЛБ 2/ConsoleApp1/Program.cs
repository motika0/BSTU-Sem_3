using System;

// Определение частичного класса Stack
public partial class Stack<T>
{
    private static int objectCount = 0;
    private readonly int id;
    private const string ConstantField = "Константа";
    private Node<T> top;

    // Внутренний класс для узла стека
    private class Node<U>
    {
        public U Value;
        public Node<U> Next;

        public Node(U value)
        {
            Value = value;
        }
    }

    static Stack()
    {
        Console.WriteLine("Статический конструктор класса Stack вызван.");
    }

    private Stack(int id)
    {
        this.id = id;
        objectCount++;
    }

    public Stack() : this(new Random().Next(1, 10000)) { }
    public Stack(T initialValue) : this()
    {
        Push(initialValue);
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= objectCount)
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
            return Peek();
        }
    }

    public void Push(T value)
    {
        Node<T> newNode = new Node<T>(value) { Next = top };
        top = newNode;
    }

    public T Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Стек пуст.");

        T value = top.Value;
        top = top.Next;
        return value;
    }

    public bool IsEmpty()
    {
        return top == null;
    }

    public T Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Стек пуст.");
        return top.Value;
    }

    public int ID => id;

    public void GetStackDetails(out int count, out T topValue)
    {
        count = objectCount;
        topValue = IsEmpty() ? default : top.Value;
    }

    public static void ShowObjectCount()
    {
        Console.WriteLine($"Создано объектов Stack: {objectCount}");
    }

    // Метод для очистки стека, используя ref
    public void ClearStack(ref Stack<T> stack)
    {
        while (!stack.IsEmpty())
        {
            stack.Pop(); // Удаляем элементы из стека
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is Stack<T> stack)
            return this.id == stack.id;
        return false;
    }

    public override int GetHashCode()
    {
        return id.GetHashCode();
    }

    public override string ToString()
    {
        return $"ID: {id}, Top Value: {(IsEmpty() ? "None" : top.Value.ToString())}";
    }
}

public class Program
{
    public static void Main()
    {
        var stackArray = new Stack<double>[3]
        {
            new Stack<double>(),
            new Stack<double>(),
            new Stack<double>()
        };

        // Добавляем элементы в стеки
        stackArray[0].Push(-2.5);
        stackArray[0].Push(3.0);
        stackArray[1].Push(4.5);
        stackArray[1].Push(1.0);
        stackArray[2].Push(-1.5);
        stackArray[2].Push(2.2);

        var minStack = GetMinStack(stackArray);
        Console.WriteLine($"Стек с наименьшим верхним элементом: {minStack.Peek()}");

        Console.WriteLine("Стек с отрицательными элементами:");
        foreach (var stack in stackArray)
        {
            if (!stack.IsEmpty() && (stack.Peek() as double?) < 0)
            {
                Console.WriteLine(stack.ToString());
            }
        }

        Stack<double>.ShowObjectCount();

        // Пример использования out
        int count = 0;
        double topValue;
        stackArray[0].GetStackDetails(out count, out topValue);
        Console.WriteLine($"Количество объектов: {count}, Верхнее значение: {topValue}");

        // Пример использования ref
        stackArray[0].ClearStack(ref stackArray[0]);
        Console.WriteLine("Стек после очистки:");
        Console.WriteLine(stackArray[0].ToString());

        var anonymousStack = new { ID = 1, TopValue = 3.0 };
        Console.WriteLine($"Анонимный тип: ID = {anonymousStack.ID}, TopValue = {anonymousStack.TopValue}");
    }

    private static Stack<double> GetMinStack(Stack<double>[] stacks)
    {
        Stack<double> minStack = stacks[0];
        foreach (var stack in stacks)
        {
            if (!stack.IsEmpty() && stack.Peek() < minStack.Peek())
            {
                minStack = stack;
            }
        }
        return minStack;
    }
}