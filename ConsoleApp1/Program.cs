// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using ConsoleApp1.Day1;
using ConsoleApp1.Day3;

Console.WriteLine("Hello, World!");

// DayBase Day = new Day1();
//DayBase Day = new Day2();

DayBase Day = new Day3();

string result = Day.GetSolution();

Console.WriteLine("result: ");
Console.WriteLine(result);
Console.ReadLine();