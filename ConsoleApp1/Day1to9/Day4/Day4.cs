using ConsoleApp1.Day2;
using ConsoleApp1.Day2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day4
{
    internal partial class Day4 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day1to9\\Day4\\inputs.txt";

        public override string Solve()
        {
            int result = 0;
            int count = 0;

            if (File.Exists(filePath))
            {
                // Open the file with a StreamReader
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        result += ProcessLine(line);

                        Console.WriteLine($"lines checked {++count} | Running total {result}");
                    }
                }

                Console.WriteLine("File processing completed.");
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }

            return result.ToString();
        }

        // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        private int ProcessLine(string line)
        {
            int result = 0;

            string[] numbers = line.Split(':')[1].Split('|');
            
            string[] winners = numbers[0].Split(' ').Where(num => num != string.Empty).ToArray();
            string[] guesses = numbers[1].Split(" ").Where(num => num != string.Empty).ToArray();

            int matches = winners.Intersect(guesses).Count();

            result = (int)Math.Pow(Convert.ToDouble(2), Convert.ToDouble(matches - 1));
            
            return result;
        }
    }
}