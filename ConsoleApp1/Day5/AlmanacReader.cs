using ConsoleApp1.Day5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day5
{
    internal static class AlmanacReader
    {
        public static Almanac ReadAlmanac(StreamReader reader, int type)
        {
            Almanac almanac = new Almanac();

            string? firstLine = reader.ReadLine();

            // seeds: 79 14 55 13
            // split by colon and then space to get the numbers, remove any entries that may be null, then convert from string to int.

            if (type == 1)
            {
                almanac.Seeds = firstLine.Split(":")[1].Split(" ")
                    .Where(seed => !string.IsNullOrEmpty(seed)).ToList()
                    .Select(long.Parse).ToList();
            }
            else
            {
                List<long> numbers = firstLine.Split(":")[1].Split(" ")
                    .Where(seed => !string.IsNullOrEmpty(seed)).ToList()
                    .Select(long.Parse).ToList();

                for (int i = 0; i + 1 < numbers.Count; i = i+2)
                {
                    List<long> seeds = GetSeeds(numbers[i], numbers[i + 1]);
                    almanac.Seeds.AddRange(seeds);
                }
            }

            string? line;

            while((line = reader.ReadLine()) != null)
            {
                if (line.Split("-").Length == 3)
                {
                    ResourceMap map = new ResourceMap();

                    map.SourceCategory = line.Split("-")[0];
                    map.DestinationCategory = line.Split("-")[2];

                    string? range;

                    while ((range = reader.ReadLine()) != null && range.Length > 3)
                    {
                        MapRange mapRange = MapRange.ConvertStringToMap(range);

                        map.Ranges.Add(mapRange);
                    }

                    almanac.AllResourceMaps.Add(map);
                }
            }

            return almanac;
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
