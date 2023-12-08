using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day8
{
    internal class Day8 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day8\\inputs.txt";

        Dictionary<string, (string left, string right, int[] indexesChecked)>? directions;
        string? instructions;

        public override string Solve()
        {
            string result;

            if (File.Exists(filePath))
            {
                // Open the file with a StreamReader
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;
                    directions = new();

                    // Read first line and skip over empty line seperating instructions and directions.
                    line = reader.ReadLine();
                    instructions = line;
                    line = reader.ReadLine();

                    while ((line = reader.ReadLine()) != null)
                    {
                        ProcessLine(line);
                    }
                }

                Console.WriteLine("File processing completed.");
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }

            result = this.MeasureDistanceToZZZ(instructions, directions).ToString();

            return result.ToString();
        }

        private int MeasureDistanceToZZZ(string? instructions, Dictionary<string, (string left, string right, int[] indexesChecked)>? directions)
        {
            int distance = 0;

            string location = "AAA";

            while (location != "ZZZ")
            {
                if (directions[location].indexesChecked.Contains(distance % instructions.Length))
                {
                    Console.WriteLine($"location {location} has already been checked on index {distance % instructions.Length}");
                    throw new ApplicationException("loop detected.");
                }
                else
                {
                    Console.WriteLine($"Taking index {distance % instructions.Length} on key {location}");
                    int[] indexesChecked = directions[location].indexesChecked.Append(distance % instructions.Length).ToArray();
                    directions[location] = (directions[location].left, directions[location].right, indexesChecked);
                }

                if (instructions[distance %  instructions.Length] == 'L')
                {
                    string locationOld = location;
                    location = directions[location].left;
                    Console.WriteLine($"Going {instructions[distance % instructions.Length]} from {locationOld} instruction to {location}");
                }
                else
                {
                    location = directions[location].right;
                    Console.WriteLine($"Taking {instructions[distance % instructions.Length]} instruction to {location}");
                }

                distance++;
            }

            return distance;
        }

        // NXS = (TKM, FPB)
        private void ProcessLine(string line)
        {
            string key = line.Substring(0, 3);
            string left = line.Substring(7, 3);
            string right = line.Substring(12, 3);

            directions?.Add(key, (left, right, new int[1] {int.MaxValue}));
        }

        public override string SolvePart2()
        {
            throw new NotImplementedException();
        }
    }
}
