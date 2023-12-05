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
            long result = int.MaxValue;
            Almanac Almanac = ReadAlamanc();

            foreach (long seed in Almanac.Seeds)
            {
                long lastResource = Almanac.ConvertSeed(seed);

                if (lastResource < result)
                {
                    Console.WriteLine($"Seed {seed} is the new closest, with location {lastResource}");
                    result = lastResource;
                }
            }

            return result.ToString();
        }
    }
}
