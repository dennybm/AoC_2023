using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day9
{
    internal partial class Day9 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day1to9\\Day9\\inputs.txt";
        int result = 0;

        public override string Solve()
        {
            this.ProcessEachLine(filePath, ProcessLine);

            return result.ToString();
        }


        private void ProcessLine(string line)
        {
            int[] firstHistory = line.Split(' ').Where(num => !string.IsNullOrEmpty(num)).Select(int.Parse).ToArray();

            int[][] historyDiffs = new int[][] { firstHistory };
            
            while (historyDiffs.Last().Where(num => num == 0).Count() != historyDiffs.Last().Count())
            {
                historyDiffs = CreateNextDiff(historyDiffs);
            }

            result += ExtrapolateHistory(historyDiffs);
        }

        private int[][] CreateNextDiff(int[][] historyDiffs)
        {
            int[] lastDiff = historyDiffs.Last();

            int[] nextDiff = new int[historyDiffs.Last().Length - 1];

            for (int i = 0; i < lastDiff.Length - 1; i++)
            {
                nextDiff[i] = lastDiff[i + 1] - lastDiff[i];
            }

            historyDiffs = historyDiffs.Append(nextDiff).ToArray();

            return historyDiffs;
        }

        private int ExtrapolateHistory(int[][] historyDiffs)
        {
            historyDiffs[historyDiffs.Length - 1] = historyDiffs.Last().Append(0).ToArray();

            for (int row = historyDiffs.Length - 2; row >= 0; row--)
            {
                historyDiffs[row] = historyDiffs[row].Append(historyDiffs[row].Last() + historyDiffs[row + 1].Last()).ToArray();
            }

            Console.WriteLine("Extrapolated Row");
            WriteTableToConsole(historyDiffs);

            return historyDiffs[0].Last();
        }

        private void WriteTableToConsole(int[][] table)
        {
            foreach (int[] row in table)
            {
                foreach (int num in row)
                {
                    Console.Write(num + " ");
                }
            
                Console.WriteLine();
            }
        }
    }
}
