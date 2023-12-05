using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day5.Models
{
    internal class ResourceMap
    {
        public string SourceCategory{ get; set; }

        public string DestinationCategory { get; set; }

        public List<MapRange> Ranges { get; set; } = new List<MapRange>();

        public long GetDestinationId(long sourceId)
        {
            long result = sourceId;

            foreach (MapRange range in this.Ranges)
            {
                // if sourceId is in the source range
                if (sourceId >= range.SourceStart && sourceId < range.SourceStart+ range.Length)
                {
                    // then convert to corresponding position in destination range
                    result = range.DestinationStart + (sourceId - range.SourceStart);
                    break;
                }
            }

            return result;
        }
    }
}
