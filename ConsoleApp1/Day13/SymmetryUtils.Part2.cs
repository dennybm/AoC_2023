using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day13
{
    internal static partial class SymmetryUtils
    {
        public static int IndexOfSmudgedHorrizontalSymmetry(this List<string> pattern)
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

                bool isSymmetric = IsSmudgedHorrizontalSymmetry(patternSubset, checkRange);

                if (isSymmetric)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private static bool IsSmudgedHorrizontalSymmetry(List<string> pattern, int checkRange)
        {
            int possibleSmudges = 0;

            for (int symmetryDistance = 0; symmetryDistance < checkRange; symmetryDistance++)
            {
                possibleSmudges += GetPossibleSmudges(pattern[checkRange - symmetryDistance - 1], pattern[checkRange + symmetryDistance]);

                if (possibleSmudges > 1)
                {
                    return false;
                }
            }

            return possibleSmudges == 1;
        }

        /// <summary>
        /// Checks possible number of smudges between rows, returns 2 for any value 2 or above.
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        /// <returns></returns>
        private static int GetPossibleSmudges(string row1, string row2)
        {
            int result = 0;

            for (int i = 0; i < row1.Length; i++)
            {
                if (row1[i] != row2[i])
                    result++;

                if (result > 1)
                {
                    return result;
                }
            }

            return result;
        }
    }
}
