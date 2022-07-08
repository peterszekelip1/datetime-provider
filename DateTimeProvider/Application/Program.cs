// See https://aka.ms/new-console-template for more information

var utcNow = DateTimeProvider.DateTimeProvider.Instance.UtcNow;
Console.WriteLine($"UtcNow: {utcNow:O}");
