using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

public class Month
{
    public string Name { get; set; }

    public Month(string name)
    {
        Name = name;
    }
}
public class WeekDay
{
    public string Name { get; set; }
    public bool IsWeekend { get; set; }
    public WeekDay(string name, bool isWeekend)
    {
        Name = name;
        IsWeekend = isWeekend;
    }
}

public class Flight
{
    public string Destination { get; set; }
    public string DayOfWeek { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public string AircraftType { get; set; }

    public Flight(string destination, string dayOfWeek, TimeSpan departureTime, string aircraftType)
    {
        Destination = destination;
        DayOfWeek = dayOfWeek;
        DepartureTime = departureTime;
        AircraftType = aircraftType;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string[] months = {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };
        int n = 6;
        var monthsWithLengthN = months.Where(m => m.Length == n);
        Console.WriteLine("Месяцы с длиной строки равной " + n + ": " + string.Join(", ", monthsWithLengthN));

        var summerWinterMonths = months.Where(m => m == "June" || m == "July" || m == "August" ||
                                                    m == "December" || m == "January" || m == "February");
        Console.WriteLine("Летние и зимние месяцы: " + string.Join(", ", summerWinterMonths));

        var sortedMonths = months.OrderBy(m => m);
        Console.WriteLine("Месяцы в алфавитном порядке: " + string.Join(", ", sortedMonths));

        var monthsWithU = months.Where(m => m.Contains("u") && m.Length >= 4);
        Console.WriteLine("Месяцы, содержащие 'u' и длиной не менее 4: " + string.Join(", ", monthsWithU));

        List<Month> monthList = new List<Month>
        {
            new Month("January"),
            new Month("February"),
            new Month("March"),
            new Month("April"),
            new Month("May"),
            new Month("June"),
            new Month("July"),
            new Month("August"),
            new Month("September"),
            new Month("October"),
            new Month("November"),
            new Month("December"),
        };

        var longNamedMonths = monthList.Where(m => m.Name.Length > 5).Select(m => m.Name);
        Console.WriteLine("Месяцы с именем длиной более 5: " + string.Join(", ", longNamedMonths));

        var customQuery = monthList
            .Where(m => m.Name.StartsWith("J"))
            .Select(m => m.Name.ToUpper()) 
            .OrderBy(m => m)
            .GroupBy(m => m.Length) 
            .Select(g => new { Length = g.Key, Count = g.Count() });

        Console.WriteLine("Запрос с 5 операторами:");
        foreach (var group in customQuery)
        {
            Console.WriteLine($"Длина: {group.Length}, Количество: {group.Count}");
        }

 
        List<Flight> flights = new List<Flight>
        {
            new Flight("New York", "Monday", new TimeSpan(10, 30, 0), "Boeing"),
            new Flight("Los Angeles", "Monday", new TimeSpan(12, 0, 0), "Airbus"),
            new Flight("New York", "Tuesday", new TimeSpan(14, 0, 0), "Boeing"),
            new Flight("Chicago", "Monday", new TimeSpan(9, 0, 0), "Boeing"),
            new Flight("Chicago", "Wednesday", new TimeSpan(11, 0, 0), "Airbus"),
            new Flight("Los Angeles", "Tuesday", new TimeSpan(15, 0, 0), "Airbus"),
            new Flight("New York", "Wednesday", new TimeSpan(18, 0, 0), "Boeing"),
        };
        List<WeekDay> weekDays = new List<WeekDay>
        {
            new WeekDay("Monday", false),
            new WeekDay ("Tuesday", false),
            new WeekDay ("Wednesday", false),
            new WeekDay ("Thursday", false),
            new WeekDay ("Friday", false),
            new WeekDay ("Saturday", true),
            new WeekDay ("Sunday", true),
        };

        var flightWithDays = from month in monthList
                             from weekDay in weekDays 
                             join flight in flights on weekDay.Name equals flight.DayOfWeek into groupedFlights
                             select new
                             {
                                 MonthName = month.Name,
                                 WeekDayName = weekDay.Name,
                                 IsWeekend = weekDay.IsWeekend,
                                 Flights = groupedFlights
                             };


        Console.WriteLine("Рейсы по месяцам и дням недели:");
        foreach (var item in flightWithDays)
        {
            Console.WriteLine($"Месяц: {item.MonthName}, День: {item.WeekDayName}, Выходной: {item.IsWeekend}");
            foreach (var flight in item.Flights)
            {
                Console.WriteLine($"   Рейс в {flight.Destination} в {flight.DepartureTime}");
            }
        }

        string destination = "New York";
        var flightsToDestination = flights.Where(f => f.Destination == destination);
        Console.WriteLine($"Рейсы в {destination}: {string.Join(", ", flightsToDestination.Select(f => f.DayOfWeek + " в " + f.DepartureTime))}");

        string day = "Monday";
        var flightsOnDay = flights.Where(f => f.DayOfWeek == day);
        Console.WriteLine($"Рейсы в {day}: {string.Join(", ", flightsOnDay.Select(f => f.Destination + " в " + f.DepartureTime))}");

        var maxDepartureFlight = flightsOnDay.OrderByDescending(f => f.DepartureTime).FirstOrDefault();
        if (maxDepartureFlight != null)
        {
            Console.WriteLine($"Максимальный рейс в {day}: {maxDepartureFlight.Destination} в {maxDepartureFlight.DepartureTime}");
        }

        var latestFlightOnDay = flights.Where(f => f.DayOfWeek == day).OrderByDescending(f => f.DepartureTime).FirstOrDefault();
        if (latestFlightOnDay != null)
        {
            Console.WriteLine($"Самый поздний рейс в {day}: {latestFlightOnDay.Destination} в {latestFlightOnDay.DepartureTime}");
        }

        var orderedFlights = flights.OrderBy(f => f.DayOfWeek).ThenBy(f => f.DepartureTime);
        Console.WriteLine("Упорядоченные по дню и времени рейсы:");
        foreach (var flight in orderedFlights)
        {
            Console.WriteLine($"{flight.DayOfWeek}: {flight.Destination} в {flight.DepartureTime}");
        }

        string aircraftTypeToCount = "Boeing";
        var countOfAircraftType = flights.Count(f => f.AircraftType == aircraftTypeToCount);
        Console.WriteLine($"Количество рейсов для типа самолета {aircraftTypeToCount}: {countOfAircraftType}");
    }
}