using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Extensions;

namespace ConsoleApp1.Day9
{
    internal partial class Day9
    {
        public override string SolvePart2()
        {
            this.ProcessEachLine(filePath, ProcessLinePart2);

            return result.ToString();
        }

        private void ProcessLinePart2(string line)
        {
            int[] firstHistory = line.Split(' ').Where(num => !string.IsNullOrEmpty(num)).Select(int.Parse).ToArray();

            int[][] historyDiffs = new int[][] { firstHistory };

            while (historyDiffs.Last().Where(num => num == 0).Count() != historyDiffs.Last().Count())
            {
                historyDiffs = CreateNextDiff(historyDiffs);
            }

            result += ExtrapolateHistoryPart2(historyDiffs);
        }

        private int ExtrapolateHistoryPart2(int[][] historyDiffs)
        {
            historyDiffs[historyDiffs.Length - 1] = historyDiffs.Last().Prepend(0).ToArray();

            for (int row = historyDiffs.Length - 2; row >= 0; row--)
            {
                historyDiffs[row] = historyDiffs[row].Prepend(historyDiffs[row].First() - historyDiffs[row + 1].First()).ToArray();
            }

            Console.WriteLine("Extrapolated Row");
            WriteTableToConsole(historyDiffs);

            return historyDiffs[0].First();
        }

    }
}
