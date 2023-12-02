using ConsoleApp1.Day2;
using ConsoleApp1.Day2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day1
{
    internal class Day2 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day2\\inputs.txt";
        int[] parameters = new int[3] { 12, 13, 14 };

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
                        result += ProcessLinePart2(line);

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

        private int ProcessLine(string line)
        {
            int result = 0;

            Game game = Utils.ConvertStringToGame(line);

            if (Utils.CheckGamePossible(game, parameters))
            {
                result = game.Id;
            }

            return result;
        }

        private int ProcessLinePart2(string line)
        {
            Game game = Utils.ConvertStringToGame(line);

            return Utils.GetGamePower(game);
        }
    }
}