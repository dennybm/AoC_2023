using ConsoleApp1.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day12
{
    internal class Day12 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day12\\inputs.txt";
        long result = 0;
        int recursiveDepth = -1;
        string topLevelSprings;
        int[] topLevelBlockLengths;

        public override string Solve()
        {
            this.ProcessEachLine(filePath, ProcessLine);

            return result.ToString();
        }

        private void ProcessLine(string line)
        {
            recursiveDepth = -1;

            Console.WriteLine($"Processing line {line}");

            string springs = line.Split(' ')[0];
            int[] blockLengths = line.Split(' ')[1].Split(',').Select(str => int.Parse(str)).ToArray();

            topLevelSprings = springs;
            topLevelBlockLengths = blockLengths;
            TryFitBlocks(springs, blockLengths);
        }

        private void ProcessLinePart2(string line)
        {
            recursiveDepth = -1;

            long lastCount = result;

            string springs = line.Split(' ')[0];
            int[] blockLengths = line.Split(' ')[1].Split(',').Select(str => int.Parse(str)).ToArray();

            string unfoldedSprings = springs + "?" + springs + "?" + springs + "?" + springs + "?" + springs;
            int[] unfoldedBlockLengths = new int[blockLengths.Length * 5];

            for (int i = 0; i < unfoldedBlockLengths.Length; i++)
            {
                unfoldedBlockLengths[i] = blockLengths[i % blockLengths.Length];
            }

            topLevelSprings = unfoldedSprings;
            topLevelBlockLengths = unfoldedBlockLengths;
            TryFitBlocks(unfoldedSprings, unfoldedBlockLengths);
            Console.WriteLine($"Processing line {line}, possibilities {result - lastCount}");
        }


        /// <summary>
        // position first block in the left most possible location
        // check if the next block fits in the left most position
        // recurse until all the blocks fit
        // then offset the last block until it has been checked against all possible positions
        // then keep going back up the list of blocks until the first block has been tested everywhere.
        public void TryFitBlocks(string springs, int[] blockLengths, List<int>? blockIndexes = null)
        {
            recursiveDepth++;

            if (blockIndexes == null)
            {
                blockIndexes = new List<int>();
            }

            // foreach sqaure that could be fit in.
            for (int i = 0; i < springs.Length - blockLengths.Sum() - blockLengths.Length + 2; i++)
            {
                if (this.DoesBlockFit(springs, blockLengths[0], i))
                {
                    // checking fit for first box at index i
                    if (blockIndexes.Count < recursiveDepth + 1)
                    {
                        blockIndexes.Add(i);
                    }
                    else
                    {
                        blockIndexes[recursiveDepth] = i;
                    }

                    // If its the last block, increase the result, if it isn't try to fit the next box.
                    if (blockLengths.Length == 1)
                    {
                        int[] testFitLengths = this.GetBlockLengths(blockIndexes);

                        if (testFitLengths.ArraysEqual(topLevelBlockLengths))
                        {
                            result++;
                            this.LogBlocksFitted(blockIndexes);
                        }

                        // Console.WriteLine($"All blocks fit, incrementing result to {result}!");
                    }
                    else
                    {
                        TryFitBlocks(springs.Substring(i + blockLengths[0] + 1), blockLengths.Skip(1).ToArray(), blockIndexes);
                    }
                }
            }

            recursiveDepth--;
        }

        private void LogBlocksFitted(List<int> blockIndexes)
        {
            List<int> filledSquares = new List<int>();
            int offset = 0;

            for (int i = 0; i < topLevelBlockLengths.Length; i++)
            {
                offset += blockIndexes[i];

                filledSquares.AddRange(Enumerable.Range(offset, topLevelBlockLengths[i]).ToList());

                offset += topLevelBlockLengths[i] + 1;
            }

            for (int i = 0; i < topLevelSprings.Length; i++) 
            {
                Console.BackgroundColor = filledSquares.Contains(i) ? ConsoleColor.DarkGreen : ConsoleColor.Black;
                Console.Write(topLevelSprings[i]);
                Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.WriteLine();
        }

        private int[] GetBlockLengths(List<int> blockIndexes)
        {
            List<int> workingSprings = new List<int>();
            int offset = 0;

            for (int i = 0; i < topLevelBlockLengths.Length; i++)
            {
                offset += blockIndexes[i];

                workingSprings.AddRange(Enumerable.Range(offset, topLevelBlockLengths[i]).ToList());

                offset += topLevelBlockLengths[i] + 1;
            }

            string testResult = string.Empty;

            for (int i = 0; i < topLevelSprings.Length; i++)
            {
                if (topLevelSprings[i] == '?')
                {
                    testResult += workingSprings.Contains(i) ? '#' : '.';
                }
                else
                {
                    testResult += topLevelSprings[i];
                }
            }

            int[] result = testResult.Split('.').Where(str => !string.IsNullOrEmpty(str)).Select(str => str.Count()).ToArray();

            return result;
        }

        /// <summary>
        /// Checks if a block of springs fits in a location. Does not check square previous to location to see if the configuration is valid.
        /// </summary>
        /// <param name="springs">springs</param>
        /// <param name="blockLength">block length</param>
        /// <param name="position">start index of block in the springs</param>
        /// <returns></returns>
        private bool DoesBlockFit(string springs, int blockLength, int position)
        {
            // springs do not overspill line
            // and the squares being fit into contain working or unknown springs
            // and the next square (if there is one), is not a working spring.

            bool result = position + blockLength < springs.Length + 1
                && springs.Substring(position, blockLength).Where(c => c == '?' || c == '#').Count() == blockLength
                && ((position + blockLength) == springs.Length || springs[position + blockLength] != '#')
                && ((position == 0 ) || springs[position - 1] != '#');

            // LogBlockFitResult(springs, blockLength, position, result);

            return result;
        }

        private void LogBlockFitResult(string springs, int blockLength, int position, bool result)
        {
            Console.WriteLine($"block fits is {result} for block length {blockLength}, in position {position} of the springs:");

            for (int i = 0; i < springs.Length; i++)
            {
                if (i >= position && i < position + blockLength)
                {
                    Console.BackgroundColor = result ? ConsoleColor.DarkGreen : ConsoleColor.DarkRed;
                    Console.Write(springs[i]);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(springs[i]);
                }
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        public override string SolvePart2()
        {
            this.ProcessEachLine(filePath, ProcessLinePart2);

            return result.ToString();
        }
    }
}
