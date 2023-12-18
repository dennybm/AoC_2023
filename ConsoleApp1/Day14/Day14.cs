using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day14
{
    internal partial class Day14 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day14\\inputs.txt";
        string[] platform;
        int result = 0;

        public override string Solve()
        {
            this.platform = this.ReadAllFile(filePath);

            result = this.TiltPlatformNorthAndWeigh(platform);

            return result.ToString();
        }

        private int TiltPlatformNorthAndWeigh(string[] platform)
        {
            result = 0;

            for (int columnIndex = 0; columnIndex < platform[0].Length; columnIndex++)
            {
                int rollingRocks = 0;
                int columnCount = 0;

                for (int rowIndex = platform.Length - 1; rowIndex > -1 ; rowIndex--)
                {

                    char platformSquare = platform[rowIndex][columnIndex];

                    if (platformSquare == 'O')
                    {
                        rollingRocks++;
                    }
                    else if (platformSquare == '#')
                    {
                        columnCount += CalculateRockWeight(rowIndex + 1, rollingRocks);
                        rollingRocks = 0;
                    }
                }

                // if there are still rolling rocks when the top is reached, count them up.
                columnCount += CalculateRockWeight(0, rollingRocks);

                // Console.WriteLine($"Column {columnIndex} weighs {columnCount}");

                result += columnCount;

            }

            return result;
        }

        private int CalculateRockWeight(int rowIndex, int rollingRocks)
        {
            if (rollingRocks == 0)
            {
                return 0;
            }

            int rowHeight = this.platform.Length - rowIndex;

            return (rowHeight + rowHeight - rollingRocks + 1) * rollingRocks / 2 ;
        }
    }
}
