using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day5.Models
{
    internal class MapRange
    {
        public long DestinationStart { get; set; }
        public long SourceStart { get; set; }
        public long Length { get; set; }

        public static MapRange ConvertStringToMap(string range)
        {
            MapRange map = new MapRange();

            List<long> numbers = range.Split(" ")
                .Where(number => !string.IsNullOrEmpty(number)).ToList()
                .Select(long.Parse).ToList();

            map.DestinationStart = numbers[0];
            map.SourceStart = numbers[1];
            map.Length = numbers[2];

            return map;
        }
    }
}
