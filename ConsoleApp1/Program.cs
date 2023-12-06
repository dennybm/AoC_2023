﻿// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using ConsoleApp1.Day1;
using ConsoleApp1.Day2;
using ConsoleApp1.Day3;
using ConsoleApp1.Day4;
using ConsoleApp1.Day5;
using ConsoleApp1.Day6;

Console.WriteLine("Hello, World!");

Dictionary<string, DayBase> days = new Dictionary<string, DayBase>()
{
    {"1", new Day1() },
    {"2", new Day2() },
    {"3", new Day3() },
    {"4", new Day4() },
    {"5", new Day5() },
    {"6", new Day6() },
};

Console.WriteLine("Enter day you would like to solve:");
string? dayRequested = Console.ReadLine();

if (!string.IsNullOrEmpty(dayRequested) && days.ContainsKey(dayRequested))
{
    Console.WriteLine("Would you like part 1 or 2?");
    string? partRequested = Console.ReadLine();

    if (!string.IsNullOrEmpty(partRequested) && (partRequested == "1" || partRequested == "2"))
    {
        string result = partRequested == "1" ?
            days[dayRequested].Solve() :
            days[dayRequested].SolvePart2();

        Console.WriteLine("result: ");
        Console.WriteLine(result);
    }
    else
    {
        Console.WriteLine("Part not recognised, please only enter the numbers 1 or 2.");
    }
}
else
{
    Console.WriteLine("Day not found, please enter a single number, ie \"1\"");
}

Console.ReadLine();