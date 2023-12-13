using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day13
{
    internal class Day13 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day13\\inputs.txt";
        List<List<string>> patterns = new List<List<string>>();

        public override string Solve()
        {
            int result = 0;

            patterns.Add(new List<string>());
            this.ProcessEachLine(filePath, ProcessLine);

            for (int i = 0; i < patterns.Count; i++)
            {
                Console.WriteLine("Checking Pattern " + i);
                List<string> pattern = patterns[i];
                result += this.GetPatternValue(pattern);
            }

            return result.ToString();
        }

        private int GetPatternValue(List<string> pattern)
        {
            int result = pattern.IndexOfHorrizontalSymmetry() * 100;

            if (result == 0)
            {
                result =  pattern.TransposePattern().IndexOfHorrizontalSymmetry();

                Console.WriteLine("Vertical symmetry found at: " + result);
            }
            else
            {
                Console.WriteLine("Found horizontal symmetry, adding: " + result);
            }

            return result;
        }

        private void ProcessLine(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                patterns.Add(new List<string>());
            }
            else
            {
                patterns.Last().Add(line);
            }
        }

        public override string SolvePart2()
        {
            throw new NotImplementedException();
        }
    }
}
