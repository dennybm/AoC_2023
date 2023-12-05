using ConsoleApp1.Day5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day5
{
    internal partial class Day5
    {
        public override string SolvePart2()
        {
            long result = long.MaxValue;
            Almanac almanac = ReadAlamanc();

            for (int i = 0; i + 1 < almanac.Seeds.Count; i = i + 2)
            {
                List<long> seeds = GetSeeds(almanac.Seeds[i], almanac.Seeds[i + 1]);

                for (int j = 0; j < seeds.Count; j++)
                {
                    if (j%1000000 == 0)
                    {
                        Console.WriteLine($"Checked {j} of {seeds.Count}");
                    }

                    long seed = seeds[j];
                    long lastResource = almanac.ConvertSeed(seed);

                    if (lastResource < result)
                    {
                        Console.WriteLine($"Seed {seed} is the new closest, with location {lastResource}");
                        result = lastResource;
                    }
                }

                Console.WriteLine($"Finished counting {almanac.Seeds[i]} range");
            }

            return result.ToString();
        }

        private static List<long> GetSeeds(long start, long length)
        {
            Console.WriteLine($"Getting more seeds: Seed start {start}, range length {length}");

            List<long> result = new List<long>();

            for (long i = start; i < start + length; i++)
            {
                result.Add(i);
            }

            return result;
        }
    }
}
