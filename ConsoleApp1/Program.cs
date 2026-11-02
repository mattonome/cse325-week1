Console.WriteLine("Hello, World!");

var name = "Mark";
var date = DateTime.Now;

// String interpolation:
Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");

// Days until next Christmas:
var nextChristmas = new DateTime(date.Year, 12, 25);
if (date.Date > nextChristmas)
{
    nextChristmas = nextChristmas.AddYears(1);
}
var daysUntilChristmas = (nextChristmas - date.Date).Days;
Console.WriteLine($"There are {daysUntilChristmas} days until the next Christmas.");

Console.WriteLine("She said, \"Hi!\"");