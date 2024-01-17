using ConsoleApp1.Day5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day5
{
    internal partial class Day5 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day1to9\\Day5\\inputs.txt";

        public override string Solve()
        {
            long result = int.MaxValue;
            Almanac almanac = ReadAlamanc();

            foreach (long seed in almanac.Seeds)
            {
                long lastResource = almanac.ConvertSeed(seed);

                if (lastResource < result)
                {
                    Console.WriteLine($"Seed {seed} is the new closest, with location {lastResource}");
                    result = lastResource;
                }
            }

            return result.ToString();
        }

        private Almanac ReadAlamanc(int part = 1)
        {
            Almanac Almanac = new Almanac();

            if (File.Exists(filePath))
            {
                // Open the file with a StreamReader
                using (StreamReader reader = new StreamReader(filePath))
                {
                    Almanac = AlmanacReader.ReadAlmanac(reader, part);
                }

                Console.WriteLine("File processing completed.");
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }

            return Almanac;
        }
    }
}
