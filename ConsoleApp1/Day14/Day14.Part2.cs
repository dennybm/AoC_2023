using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day14
{
    // 85117 was too low
    // 102507 was too high

    internal partial class Day14
    {
        char[][] platformArray;
        List<char[][]> platformsSeen = new List<char[][]>();
        List<(int index, int weight)> weightsSeen = new List<(int index, int weight)>();

        public override string SolvePart2()
        {
            this.platform = this.ReadAllFile(filePath);
            platformArray = this.platform.Select(str => str.ToArray()).ToArray();

            for (int i = 0; i < 1000000000; i++)
            {
                platformArray = this.SpinPlatform(platformArray);
                int weight = WeighPlatform(platformArray);
                // Console.WriteLine(weight);

                if (weightsSeen.Select(x => x.weight).Contains(weight))
                {
                    var spinAndWeight = weightsSeen.Where(x => x.weight == weight).Last();
                    int lastSeen = i - spinAndWeight.index;
                    //Console.WriteLine($"Weight {weight} was last seen {lastSeen} spins ago.");

                    if ((1000000000 - 1) % lastSeen == i % lastSeen)
                    {
                        Console.WriteLine($"Possible value for a billion: {weight}");
                    }
                }

                weightsSeen.Add((i, weight));
            }

            int spunPlatformWeight = WeighPlatform(platformArray);
            return spunPlatformWeight.ToString();
        }

        private char[][] SpinPlatform(char[][] platform)
        {
            char[][] tiltedPlatform = TiltNorth(platform);
            tiltedPlatform = TurnAndTilt(platform);
            tiltedPlatform = TurnAndTilt(platform);
            tiltedPlatform = TurnAndTilt(platform);
            tiltedPlatform = Rotate90degrees(tiltedPlatform);

            return tiltedPlatform;
        }

        private int WeighPlatform(char[][] platform)
        {
            int weight = 0;

            for (int rowIndex = 0; rowIndex < platform.Length; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < platform[rowIndex].Length; columnIndex++)
                {
                    if (platform[rowIndex][columnIndex] == 'O')
                    {
                        weight += platform.Length - rowIndex;
                    }
                }
            }

            return weight;
        }

        private void LogPlatform(char[][] tiltedPlatform)
        {
            foreach (char[] row in tiltedPlatform)
            {
                Console.WriteLine();

                foreach (char c in row)
                {
                    Console.Write(c);
                }
            }
            Console.WriteLine();
        }

        private char[][] TurnAndTilt(char[][] platform)
        {
            char[][] rotatedPlatform = Rotate90degrees(platform);

            return TiltNorth(rotatedPlatform).ToArray();
        }

        private char[][] TiltNorth(char[][] platform)
        {
            char[][] tiltedPlatform = platform;

            for (int columnIndex = 0; columnIndex < platform[0].Length; columnIndex++)
            {
                int rollingRocks = 0;
                int lastFixedRowIndex = platform.Count();

                for (int rowIndex = platform.Length - 1; rowIndex > -1; rowIndex--)
                {
                    char platformSquare = platform[rowIndex][columnIndex];

                    if (platformSquare == 'O')
                    {
                        rollingRocks++;
                    }
                    else if (platformSquare == '#')
                    {
                        for (int i = 0; i < rollingRocks; i++)
                        {
                            tiltedPlatform[rowIndex + i + 1][columnIndex] = 'O';
                        }
                        
                        for (int i = 0; rowIndex + rollingRocks + i < lastFixedRowIndex - 1; i++)
                        {
                            tiltedPlatform[rowIndex + rollingRocks + i + 1][columnIndex] = '.';
                        }

                        lastFixedRowIndex = rowIndex;
                        rollingRocks = 0;
                    }
                }

                // roll the last rocks
                for (int i = 0; i < rollingRocks; i++)
                {
                    tiltedPlatform[i][columnIndex] = 'O';
                }

                for (int i = 0; rollingRocks + i < lastFixedRowIndex; i++)
                {
                    tiltedPlatform[rollingRocks + i][columnIndex] = '.';
                }

                rollingRocks = 0;
            }

            return tiltedPlatform;
        }

        public char[][] Rotate90degrees(char[][] matrix)
        {
            int w = matrix[0].Count();
            int h = matrix.Count();

            char[][] result = new char[w][];

            for (int i = 0; i < w; i++)
            {
                char[] row = new char[h];

                for (int j = 0; j < h; j++)
                {
                    row[j] = matrix[h-j-1][i];
                }

                result[i] = row;
            }

            return result;
        }
    }
}
