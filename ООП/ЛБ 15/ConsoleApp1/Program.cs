using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    // 1.
    static async Task<int[]> FindPrimesAsync(int max)
    {
        return await Task.Run(() =>
        {
            var sieve = new bool[max + 1];
            for (int i = 2; i * i <= max; i++)
            {
                if (!sieve[i])
                {
                    for (int j = i * i; j <= max; j += i)
                    {
                        sieve[j] = true;
                    }
                }
            }
            return Enumerable.Range(2, max - 1).Where(i => !sieve[i]).ToArray();
        });
    }

    // 2.
    static async Task<int[]> FindPrimesWithCancellationAsync(int max, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            var sieve = new bool[max + 1];
            for (int i = 2; i * i <= max; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                if (!sieve[i])
                {
                    for (int j = i * i; j <= max; j += i)
                    {
                        sieve[j] = true;
                    }
                }
            }
            return Enumerable.Range(2, max - 1).Where(i => !sieve[i]).ToArray();
        }, cancellationToken);
    }

    static async Task<int> CalculateAsync(int number) => await Task.Run(() => number * number);

    static void ContinuationExample()
    {
        var tasks = new[]
        {
            Task.Run(() => 5),
            Task.Run(() => 10),
            Task.Run(() => 15)
        };

        Task.WhenAll(tasks).ContinueWith(t =>
        {
            Console.WriteLine("Сумма: " + t.Result.Sum());
        }).Wait();
    }

    static void ParallelForExample()
    {
        Parallel.For(0, 1000000, i => { var square = i * i; });
    }

    static void ParallelInvokeExample()
    {
        Parallel.Invoke(
            () => Console.WriteLine("Задача 1 выполнена."),
            () => Console.WriteLine("Задача 2 выполнена."),
            () => Console.WriteLine("Задача 3 выполнена.")
        );
    }

    static void SupplierConsumerExample()
    {
        var stock = new BlockingCollection<string>();
        var suppliers = new[]
        {
            Task.Run(() => ProduceItems(stock, 0, 5, 100)),
            Task.Run(() => ProduceItems(stock, 5, 10, 150))
        };

        var consumers = Enumerable.Range(0, 10).Select(i => Task.Run(() => ConsumeItems(stock, i))).ToArray();

        Task.WaitAll(suppliers);
        stock.CompleteAdding();
        Task.WaitAll(consumers);
    }

    static void ProduceItems(BlockingCollection<string> stock, int start, int end, int delay)
    {
        for (int i = start; i < end; i++)
        {
            stock.Add($"Товар {i}");
            Thread.Sleep(delay);
            Console.WriteLine($"Поставщик добавил: Товар {i}");
        }
    }

    static void ConsumeItems(BlockingCollection<string> stock, int consumerId)
    {
        while (stock.TryTake(out var item, Timeout.Infinite))
        {
            Console.WriteLine($"Покупатель {consumerId} купил {item}");
        }
        Console.WriteLine($"Покупатель {consumerId} не нашел товар и ушел.");
    }

    static async Task AsyncExample()
    {
        await Task.Delay(1000);
        Console.WriteLine("Асинхронная задача завершена.");
    }

    static async Task Main(string[] args)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        // 1. 
        var task1 = FindPrimesAsync(100000);
        Console.WriteLine($"Задача 1 ID: {task1.Id}, Статус: {task1.Status}");

        var primes1 = await task1;
        Console.WriteLine($"Найдено простых чисел (без отмены): {primes1.Length}");
        stopwatch.Stop();
        Console.WriteLine($"Время выполнения (без отмены): {stopwatch.ElapsedMilliseconds} мс");

        // 2.
        var task2 = FindPrimesWithCancellationAsync(100000, cancellationTokenSource.Token);
        Console.WriteLine($"Задача 2 ID: {task2.Id}, Статус: {task2.Status}");

        await Task.Delay(2000);
        cancellationTokenSource.Cancel();

        try
        {
            var primes2 = await task2;
            Console.WriteLine($"Найдено простых чисел (с отменой): {primes2.Length}");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Задача 2 была отменена.");
        }

        // 3
        var results = await Task.WhenAll(CalculateAsync(5), CalculateAsync(10), CalculateAsync(15));
        Console.WriteLine($"Результаты: {string.Join(", ", results)}");

        // 4
        ContinuationExample();

        // 5
        ParallelForExample();

        // 6
        ParallelInvokeExample();

        // 7
        SupplierConsumerExample();

        // 8
        await AsyncExample();
    }
}