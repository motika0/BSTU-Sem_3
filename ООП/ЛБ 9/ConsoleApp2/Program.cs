using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

public class Concert : IComparable<Concert>
{
    public int Id { get; set; }
    public string Artist { get; set; }
    public string Venue { get; set; }
    public DateTime Date { get; set; }

    public Concert(int id, string artist, string venue, DateTime date)
    {
        Id = id;
        Artist = artist;
        Venue = venue;
        Date = date;
    }

    public int CompareTo(Concert other) => Date.CompareTo(other.Date);

    public override string ToString() => $"{Id}: {Artist} at {Venue} on {Date.ToShortDateString()}";
}

public class ConcertManager
{
    private readonly Dictionary<int, Concert> concertDict = new Dictionary<int, Concert>();

    public void AddConcert(Concert concert) => concertDict[concert.Id] = concert;

    public bool RemoveConcert(int id) => concertDict.Remove(id);

    public Concert FindConcert(int id) => concertDict.TryGetValue(id, out var concert) ? concert : null;

    public void DisplayConcerts()
    {
        foreach (var concert in concertDict.Values)
        {
            Console.WriteLine(concert);
        }
    }
}

public class NumberCollection
{
    private readonly List<int> numbers = new List<int>();

    public void Add(int number) => numbers.Add(number);

    public void RemoveMultiple(int count)
    {
        count = Math.Min(count, numbers.Count);
        numbers.RemoveRange(0, count);
    }

    public void Display() => Console.WriteLine("Current numbers: " + string.Join(", ", numbers));

    public List<int> GetNumbers() => new List<int>(numbers);
}

public class ObservableConcertManager
{
    private readonly ObservableCollection<Concert> concertList = new ObservableCollection<Concert>();

    public ObservableConcertManager()
    {
        concertList.CollectionChanged += OnConcertListChanged;
    }

    private void OnConcertListChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (Concert concert in e.NewItems)
            {
                Console.WriteLine($"Added concert: {concert.Artist} at {concert.Venue}");
            }
        }
        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (Concert concert in e.OldItems)
            {
                Console.WriteLine($"Removed concert: {concert.Artist} at {concert.Venue}");
            }
        }
    }

    public void AddConcert(Concert concert) => concertList.Add(concert);

    public void RemoveConcert(Concert concert) => concertList.Remove(concert);
}

class Program
{
    static void Main(string[] args)
    {
        var concertManager = new ConcertManager();
        concertManager.AddConcert(new Concert(1, "Artist A", "Venue A", new DateTime(2023, 5, 1)));
        concertManager.AddConcert(new Concert(2, "Artist B", "Venue B", new DateTime(2023, 6, 15)));
        concertManager.AddConcert(new Concert(3, "Artist C", "Venue C", new DateTime(2023, 7, 23)));

        Console.WriteLine("All concerts:");
        concertManager.DisplayConcerts();

        Console.WriteLine("\nRemoving concert with ID 2.");
        concertManager.RemoveConcert(2);
        Console.WriteLine("Concerts after removal:");
        concertManager.DisplayConcerts();

        Console.WriteLine("\nSearching for concert with ID 3:");
        var foundConcert = concertManager.FindConcert(3);
        Console.WriteLine(foundConcert != null ? foundConcert.ToString() : "Concert not found.");

        var numberCollection = new NumberCollection();
        for (int i = 0; i < 10; i++)
        {
            numberCollection.Add(i);
        }
        numberCollection.Display();

        numberCollection.RemoveMultiple(3);
        Console.WriteLine("After removing 3 elements:");
        numberCollection.Display();

        numberCollection.Add(10);
        numberCollection.Add(11);
        numberCollection.Add(12);
        Console.WriteLine("After adding new elements:");
        numberCollection.Display();

        var uniqueNumbers = new HashSet<int>(numberCollection.GetNumbers());
        Console.WriteLine("Elements in the HashSet: " + string.Join(", ", uniqueNumbers));

        int searchValue = 10;
        Console.WriteLine($"Does the HashSet contain {searchValue}? {uniqueNumbers.Contains(searchValue)}");

        var observableConcertManager = new ObservableConcertManager();
        observableConcertManager.AddConcert(new Concert(4, "Artist D", "Venue D", DateTime.Now));
        observableConcertManager.AddConcert(new Concert(5, "Artist E", "Venue E", DateTime.Now.AddDays(1)));

        Console.WriteLine("Removing concert:");
        var concertToRemove = new Concert(4, "Artist D", "Venue D", DateTime.Now);
        observableConcertManager.RemoveConcert(concertToRemove);
    }
}