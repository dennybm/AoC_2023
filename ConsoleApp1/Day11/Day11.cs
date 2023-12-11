using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day11
{
    internal partial class Day11 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day11\\inputs.txt";
        string[]? universe;
        string[]? expandedUniverse;
        List<(int x, int y)> galaxies = new();
        List<int> closestGalaxyDistance = new();

        public string SolvePart1Wrong()
        {
            int result = 0;

            this.universe = ReadAllFile(filePath);

            this.galaxies = FindGalaxies(universe);
            this.expandedUniverse = ExpandUniverse(universe);
            this.galaxies = FindGalaxies(expandedUniverse);

            foreach (var galaxy in this.galaxies)
            {
                closestGalaxyDistance.Add(FindClosestGalaxy(galaxy));
            }

            foreach (int distance in closestGalaxyDistance)
            {
                result += distance;
            }

            return result.ToString();
        }

        public override string Solve()
        {
            int result = 0;

            this.universe = ReadAllFile(filePath);

            this.galaxies = FindGalaxies(universe);
            this.expandedUniverse = ExpandUniverse(universe);
            this.galaxies = FindGalaxies(expandedUniverse);

            for (int i = 0; i < galaxies.Count; i++)
            {
                for (int j = i + 1; j < galaxies.Count; j++)
                {
                    result += DistanceBetweenGalaxies(galaxies[i], galaxies[j]);
                }
            }

            return result.ToString();
        }

        private int DistanceBetweenGalaxies((int x, int y) galaxy1, (int x, int y) galaxy2)
        {
            return Math.Abs(galaxy1.x - galaxy2.x) + Math.Abs(galaxy1.y - galaxy2.y);
        }

        private int FindClosestGalaxy((int x, int y) galaxy)
        {
            int result = 0;

            for (int radius = 0; radius < this.universe[0].Length; radius++)
            {
                for (int rowIndex = -radius; rowIndex < radius + 1; rowIndex++)
                {
                    (int x, int y) squareToCheckAbove = (galaxy.x + rowIndex, galaxy.y + (radius - Math.Abs(rowIndex)));
                    (int x, int y) squareToCheckBelow = (galaxy.x + rowIndex, galaxy.y - (radius - Math.Abs(rowIndex)));

                    if (SquareIsInGalaxy(squareToCheckAbove))
                    {
                        if (expandedUniverse[squareToCheckAbove.y][squareToCheckAbove.x] == '#')
                        {
                            result = radius; break;
                        }
                    }

                    if (SquareIsInGalaxy(squareToCheckBelow))
                    {
                        if (expandedUniverse[squareToCheckBelow.y][squareToCheckBelow.x] == '#')
                        {
                            result = radius; break;
                        }
                    }
                }

                if (result != 0)
                {
                    break;
                }
            }

            return result;
        }

        private bool SquareIsInGalaxy((int x, int y) square)
        {
            return square.x >= 0 && square.x < universe[0].Length && square.y >= 0 && square.y < universe.Length;
        }
        
        private List<(int x, int y)> FindGalaxies(string[] universe)
        {
            List<(int x, int y)> result = new();

            for (int yIndex = 0; yIndex < universe.Length; yIndex++)
            {
                string line = universe[yIndex];
                for (int xIndex = 0; xIndex < line.Length; xIndex++)
                {
                    if (universe[yIndex][xIndex] == '#')
                    {
                        result.Add((xIndex, yIndex));
                    }
                }
            }

            return result;
        }

        private string[]? ExpandUniverse(string[] universe)
        {
            int[] emptyRows = Enumerable.Range(0, universe.Length - 1).Except(galaxies.Select(g => g.y)).ToArray();
            int[] emptyColumns = Enumerable.Range(0, universe[0].Length - 1).Except(galaxies.Select(g => g.x)).ToArray();

            string[] result = new string[universe.Length + emptyRows.Length];

            int yOffset = 0;

            for (int yIndex = 0; yIndex < universe.Length; yIndex++)
            {
                string line = universe[yIndex];
                string expandedLine = string.Empty;

                for (int xIndex = 0; xIndex < line.Length; xIndex++)
                {
                    char c = line[xIndex];

                    if (emptyColumns.Contains(xIndex))
                    {
                        expandedLine += c;
                        expandedLine += c;
                    }
                    else
                    {
                        expandedLine += c;
                    }
                }

                if (emptyRows.Contains(yIndex))
                {
                    result[yIndex + yOffset] = expandedLine;
                    result[yIndex + yOffset + 1] = expandedLine;
                    yOffset++;
                }
                else
                {
                    result[yIndex + yOffset] = expandedLine;
                }
            }

            return result;
        }
    }
}
