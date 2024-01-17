using ConsoleApp1.Day6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp1.StringUtils;

namespace ConsoleApp1.Day6
{
    internal partial class Day6 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day1to9\\Day6\\inputs.txt";

        public override string Solve()
        {
            int result = 1;

            string[] data = this.ReadAllFile(filePath);
            List<Race> races = new List<Race>();
            long[] raceTimes = data[0].Split(':')[1]
                .ToNonZeroLongArray(" ");
            long[] raceDistances = data[1].Split(':')[1]
                .ToNonZeroLongArray(" ");

            for (int i = 0; i < raceTimes.Length; i++)
            {
                races.Add(new Race()
                {
                    Time = raceTimes[i],
                    Distance = raceDistances[i],
                });
            }

            foreach (Race race in races)
            {
                int score = race.GetRaceScore();

                if (score > 0)
                {
                    result = result * score;
                }

                Console.WriteLine($"Running total is {result}");
            }

            return result.ToString();
        }
    }
}
