using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day13
{
    internal partial class Day13
    {
        public override string SolvePart2()
        {
            int result = 0;

            patterns.Add(new List<string>());
            this.ProcessEachLine(filePath, ProcessLine);

            for (int i = 0; i < patterns.Count; i++)
            {
                Console.WriteLine("Checking Pattern " + i);
                List<string> pattern = patterns[i];
                result += this.GetPatternValuePart2(pattern);
            }

            return result.ToString();
        }

        private int GetPatternValuePart2(List<string> pattern)
        {
            int result = pattern.IndexOfSmudgedHorrizontalSymmetry() * 100;

            if (result == 0)
            {
                result = pattern.TransposePattern().IndexOfSmudgedHorrizontalSymmetry();

                Console.WriteLine("Vertical symmetry found at: " + result);
            }
            else
            {
                Console.WriteLine("Found horizontal symmetry, adding: " + result);
            }

            return result;
        }
    }
}
