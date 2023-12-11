using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day8
{
    internal partial class Day8
    {
        public string SolvePart2Attempt2()
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

            result = this.MeasureDistanceToZZZPart2(instructions, directions).ToString();

            return result.ToString();
        }

        private long MeasureDistanceToZZZPart2Attempt2(string? instructions, Dictionary<string, (string left, string right, int[] indexesChecked)>? directions)
        {
            long distance = 0;

            var startingNodes = directions.Where(direction => direction.Key.Last() == 'A').Select(direction => direction.Key).ToArray();

            Console.WriteLine("Starting Nodes: ");
            foreach (var node in startingNodes)
            {
                Console.Write(node + " ");
            }

            int[] loopLengths = new int[startingNodes.Length];
            int[][] zEndingNodesArray = new int[startingNodes.Length][];

            // For each starting node, get the loop length and the position of the z ending nodes.
            for (int i = 0; i < startingNodes.Length; i++)
            {
                string? node = startingNodes[i];
                (loopLengths[i], zEndingNodesArray[i]) = this.GetLoopLengthsAndNodeWins(i, startingNodes[i], instructions, directions);
            }

            bool keepLooking = true;

            // first distance to check is the first hit in the first loop.
            distance = zEndingNodesArray[0][0];

            // start with distance 0.
            while (keepLooking)
            {
                // add on another loop to the distance.
                distance = 21366921060721;

                // foreach loop
                for (int nodeIndex = 0; nodeIndex < zEndingNodesArray.Length; nodeIndex++)
                {
                    // check if the distance is a hit in the loop.
                    bool isHit = this.IsDistanceAHitInThisLoop(distance, zEndingNodesArray[nodeIndex], loopLengths[nodeIndex]);

                    if (!isHit)
                    {
                        // if the distance is not a hit, then break out of the loop check and move on to next distance.
                        keepLooking = true;

                        Console.WriteLine($"Distance {distance} is not a hit in loop {nodeIndex}");

                        break;
                    }

                    Console.WriteLine($"Distance {distance} was a HIT! in loop {nodeIndex}");

                    // if we get to the final loop and it is a hit then we can stop looking.
                    keepLooking = false;
                }
            }

            return distance;
        }

        private bool IsDistanceAHitInThisLoop(long distance, int[] zNodeList, int loopLength)
        {
            bool result = false;

            foreach (int zNode in zNodeList)
            {
                if (distance % loopLength == zNode)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        // The correct answer to part 2 can be found by calculating the LCM of the NodeWin indexes.
        private (int loopLength, int[] zEndingNodes)  GetLoopLengthsAndNodeWins(int i, string start, string? instructions, Dictionary<string, (string left, string right, int[] indexesChecked)>? directions)
        {
            int distance = 0;
            int[] zEndingNodes = new int[0];
            bool loopIncomplete = true;
            string location = start;

            while (loopIncomplete)
            {
                if (location.Last() == 'Z')
                {
                    zEndingNodes = zEndingNodes.Append(distance).ToArray();
                }

                if (directions[location].indexesChecked.Contains(distance % instructions.Length))
                {
                    Console.WriteLine($"location {location} has already been checked on index {distance % instructions.Length}. Loop detected.");
                    loopIncomplete = false;
                    break;
                }
                else
                {
                    Console.WriteLine($"Taking index {distance % instructions.Length} on key {location}");
                    int[] indexesChecked = directions[location].indexesChecked.Append(distance % instructions.Length).ToArray();
                    directions[location] = (directions[location].left, directions[location].right, indexesChecked);
                }

                string locationOld = location;

                if (instructions[distance % instructions.Length] == 'L')
                {
                    location = directions[location].left;
                    Console.WriteLine($"Going {instructions[distance % instructions.Length]} from {locationOld} instruction to {location}");
                }
                else
                {
                    location = directions[location].right;
                    Console.WriteLine($"Going {instructions[distance % instructions.Length]} from {locationOld} instruction to {location}");
                }

                distance++;
            }

            return (distance, zEndingNodes);
        }
    }
}
