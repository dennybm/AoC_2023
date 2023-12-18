using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day15
{
    internal partial class Day15 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day15\\inputs.txt";
        int sum = 0;

        public override string Solve()
        {
            this.ProcessEachLine(filePath, SumHashes);

            return this.sum.ToString();
        }

        private void SumHashes(string input)
        {
            string[] initSequence = input.Split(',');

            foreach (string init in initSequence)
            {
                this.sum += GetHash(init);
            }
        }

        public static int GetHash(string init)
        {
            int hash = 0;

            foreach (char c in init)
            {
                hash = ((hash + c) * 17) % 256;
            }

            return hash;
        }
    }
}
