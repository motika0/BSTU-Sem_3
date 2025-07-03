using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp2
{

    class Program
    {
        static void Main()
        {
            // a. Определение и инициализация переменных примитивных типов
            int i = 42; // целое число
            float f = 3.14f; //дробь с 1
            double d = 3.14; // дробь с 2
            char c = 'Z'; //символ
            bool b = true; //логический  тип
            byte q = 255;//байтовый
            sbyte p = -100;//знаковый(-127-128)
            short s = 32767;
            ushort u = 65535;
            long l = 1234567890123;
            ulong g = 1234567890123;
            decimal h = 100.5m;

            // Вывод значений переменных в консоль
            Console.WriteLine("int:" + i);
            Console.WriteLine("float:" + f);
            Console.WriteLine("double:" + d);
            Console.WriteLine("char:" + c);
            Console.WriteLine("bool:" + b);
            Console.WriteLine("byte:" + g);
            Console.WriteLine("sbyte:" + p);
            Console.WriteLine("short:" + s);
            Console.WriteLine("ushort:" + u);
            Console.WriteLine("long:" + l);
            Console.WriteLine("ulong:" + g);
            Console.WriteLine("decimal:" + h);

            // b. Явное и неявное приведение типов
            // Неявные приведения
            int ag = 10;
            long b1 = ag; // int -> long
            float b2 = ag; // int -> float
            double b3 = ag; // int -> double
            double b4 = b1; // long -> double
            float b5 = b1; // long -> float (неявное, но может потерять точность)

            // Явные приведения
            double doub = 9.78;
            int c1 = (int)doub; // double -> int
            long c2 = (long)doub;  // double -> long
            float c3 = (float)doub; // double -> float
            int c4 = (int)f; // float -> int
            byte c5 = (byte)i; // int -> byte (может привести к ошибке, если значение больше 255)

            Console.WriteLine($"Неявные: {b1}, {b2}, {b3}, {b4}, {b5}");
            Console.WriteLine($"Явные:{c1}, {c2}, {c3}, {c4}, {c5}");

            // c. Упаковка и распаковка значимых типов
            int val = 123;
            object boxed = val; // Упаковка
            int unboxed = (int)boxed; // Распаковка
            Console.WriteLine($"Упакованное значение: {boxed}, Распакованное значение: {unboxed}");

            // d. Работа с неявно типизированной переменной
            var imp = 5; // неявная типизация, тип int
            Console.WriteLine($"Неявно типизированная переменная: {imp}, тип: {imp.GetType()}");

            // e. Работа с Nullable переменной
            int? nullableInt = null; // Nullable переменная
            if (nullableInt.HasValue)
            {
                Console.WriteLine($"Nullable значение: {nullableInt.Value}");
            }
            else
            {
                Console.WriteLine("Nullable переменная не имеет значения.");
            }

            // f. Присвоение переменной var значения другого типаB
            var dyn = "10"; // Начальное значение типа string
            Console.WriteLine($"Значение переменной var: {dyn}");

        }
    }

}
