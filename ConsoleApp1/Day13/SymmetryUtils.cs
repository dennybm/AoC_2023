using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day13
{
    internal static class SymmetryUtils
    {
        public static int IndexOfHorrizontalSymmetry(this List<string> pattern)
        {
            int index = 0;

            if (pattern.Count() < 2)
            {
                return index;
            }

            for (int i = 1; i < pattern.Count(); i++)
            {
                int checkRange = Math.Min(i, pattern.Count() - i);

                var patternSubset = pattern.GetRange(i - checkRange, checkRange * 2);

                bool isSymmetric = IsHorrizontalSymmetry(patternSubset, checkRange);

                if (isSymmetric)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private static bool IsHorrizontalSymmetry(List<string> pattern, int checkRange)
        {
            bool result = true;

            for (int symmetryDistance = 0; symmetryDistance < checkRange; symmetryDistance++)
            {
                if (pattern[checkRange - symmetryDistance - 1] != pattern[checkRange + symmetryDistance])
                {
                    return false;
                }
            }

            return result;
        }

        public static List<string> TransposePattern (this List<string> pattern)
        {
            List<string> result = pattern
                .SelectMany(inner => inner.Select((item, index) => new { item, index }))
                .GroupBy(i => i.index, i => i.item)
                .Select(g => new string(g.ToArray()))
                .ToList();

            // LogTransposition(pattern, result);

            return result;
        }

        private static void LogTransposition(List<string> pattern, List<string> result)
        {
            Console.WriteLine("==== Transposed ====");

            foreach (string line in pattern)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("==== To ====");

            foreach (string line in result)
            {
                Console.WriteLine(line);
            }
        }
    }
}
