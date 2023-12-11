using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day5.Models
{
    internal class Almanac
    {
        public List<long> Seeds { get; set; } = new List<long>();

        public List<ResourceMap> AllResourceMaps { get; set; } = new List<ResourceMap>();

        public long ConvertSeed(long sourceId)
        {
            long nextResourceId = sourceId;

            foreach (ResourceMap map in AllResourceMaps)
            {
                nextResourceId = map.GetDestinationId(nextResourceId);
            }

            return nextResourceId;
        }
    }
}
