using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day3
{
    internal class Day3 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day3\\inputs.txt";
        string[] engineSchematic;


        public override string Solve()
        {
            string result = string.Empty;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    engineSchematic = File.ReadAllLines(filePath);
                }
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }

            result = Utils3.GetPartNumberSum(engineSchematic).ToString();

            return result;
        }
    }
}
