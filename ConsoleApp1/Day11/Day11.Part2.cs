using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day11
{
    internal partial class Day11
    {
        (List<int> emptyRows, List<int> emptyColumns) expandingSpace;

        public override string SolvePart2()
        {
            long result = 0;
            this.universe = ReadAllFile(filePath);
            this.galaxies = FindGalaxies(universe);

            expandingSpace.emptyRows = Enumerable.Range(0, universe.Length - 1).Except(galaxies.Select(g => g.y)).ToList();
            expandingSpace.emptyColumns = Enumerable.Range(0, universe[0].Length - 1).Except(galaxies.Select(g => g.x)).ToList();

            LogResult();

            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    long distance = DistanceBetweenGalaxies(galaxies[i], galaxies[j], 1000000);

                    // Console.WriteLine($"Distance between galaxy {i} and galaxy {j} is {distance}");

                    result += distance;
                }
            }

            return result.ToString();
        }

        private void LogResult()
        {
            LogUniverse();
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private void LogUniverse()
        {
            int galaxyIndex = 0;

            for (int row = 0; row < universe.Length; row++)
            {
                if (expandingSpace.emptyRows.Contains(row))
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine(universe[row]);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;

                    for (int column = 0; column < universe[row].Length; column++)
                    {
                        char c = universe[row][column];

                        Console.BackgroundColor = expandingSpace.emptyColumns.Contains(column) ? ConsoleColor.DarkBlue : ConsoleColor.DarkRed;
                        
                        if (c == '#')
                        {
                            Console.Write(galaxyIndex++);
                        }
                        else
                        {
                            Console.Write(c);
                        }
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine();
                }
            }
        }

        private long DistanceBetweenGalaxies((int x, int y) galaxy1, (int x, int y) galaxy2, int expansionFactor)
        {
            var furthest = galaxy1.x > galaxy2.x ? galaxy1 : galaxy2;
            var leastFar = galaxy1.x < galaxy2.x ? galaxy1 : galaxy2;
            var deepest = galaxy1.y > galaxy2.y ? galaxy1 : galaxy2;
            var leastDeep = galaxy1.y < galaxy2.y ? galaxy1 : galaxy2;

            int xDistance = furthest.x - leastFar.x;
            int yDistance = deepest.y - leastDeep.y;

            int unexpandedDistance = xDistance + yDistance;

            int xExpandedCrossings = Enumerable.Range(leastFar.x, xDistance).Intersect(expandingSpace.emptyColumns).Count();
            int yExpandedCrossings = Enumerable.Range(leastDeep.y, yDistance).Intersect(expandingSpace.emptyRows).Count();

            long expandedDistance = unexpandedDistance + 
                 xExpandedCrossings * (expansionFactor - 1) +
                 yExpandedCrossings * (expansionFactor - 1);

            return expandedDistance;
        }
    }
}
