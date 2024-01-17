using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day18
{
    internal class Day18 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day18\\inputs.txt";
        List<(int y, int x)> excavatedBoundary;
        List<(int y, int x)> interiorHoles;
        (int y, int x) diggerLocation = (0, 0);

        public override string Solve()
        {
            this.excavatedBoundary = new();
            this.interiorHoles = new();

            this.ProcessEachLine(filePath, ProcessDigInstruction);

            // this.DrawExcavation();

            this.DigOutRemainingEarth();

            this.DrawExcavation();

            // this.DrawFilling();

            return (excavatedBoundary.Count() + interiorHoles.Count()).ToString();
        }

        private void DigOutRemainingEarth()
        {
            int ExcavationWidth = excavatedBoundary.Max(hole => hole.x); // - holes.Min(hole => hole.x); if digger went negative on x axis we would need this extra logic.
            int ExcavationDepth = excavatedBoundary.Max(hole => hole.y);

            for (int depth = excavatedBoundary.Min(hole => hole.y) - 2; depth <= ExcavationDepth; depth++)
            {
                var boundariesAtDepth = excavatedBoundary.Where(hole => hole.y == depth).Order();

                bool isInside = false;
                (int y, int x)? previousBoundary = null;
                string previousInflectionShape = string.Empty;

                foreach (var boundary in boundariesAtDepth)
                {
                    // if previous sqaure is also a boundary move on check above and below previous square to get bend chape
                    // if it isn't then fill depending on whether we are inside the boundary, then flip the isInside variable.
                    // if point of inflection, then flip isInside again.

                    if (depth == 4)
                    {
                        Console.WriteLine(isInside);
                    }

                    if (boundariesAtDepth.Contains((boundary.y, boundary.x - 1)))
                    {
                        // previous square was filled, check for inflectrion.
                        if (excavatedBoundary.Contains((boundary.y + 1, boundary.x)))
                        {
                            if (previousInflectionShape == "D")
                            {
                                isInside = !isInside;
                            }

                            previousInflectionShape = "D";
                        }
                        else if (excavatedBoundary.Contains((boundary.y - 1, boundary.x)))
                        {
                            if (previousInflectionShape == "U")
                            {
                                isInside = !isInside;
                            }

                            previousInflectionShape = "U";
                        }
                    }
                    else
                    {
                        // previous square was not a boundary, check inflection shape
                        if (previousBoundary != null)
                        {
                            if (excavatedBoundary.Contains((boundary.y + 1, boundary.x)))
                            {
                                previousInflectionShape = "D";
                            }
                            else if (excavatedBoundary.Contains((boundary.y - 1, boundary.x)))
                            {
                                previousInflectionShape = "U";
                            }
                        }

                        // previous square was not a boundary, so dig out if we are inside the border.

                        if (isInside)
                        {
                            if (previousBoundary != null)
                            {
                                for (int x = previousBoundary.Value.x + 1; x < boundary.x; x++)
                                {
                                    this.interiorHoles.Add((depth, x));
                                }
                            }
                        }

                        isInside = !isInside;

                        //// check for inflection, if next square is a boundary
                        if (boundariesAtDepth.Contains((boundary.y, boundary.x + 1)))
                        {
                            if(excavatedBoundary.Contains((boundary.y - 1, boundary.x)))
                            {
                                previousInflectionShape = "U";
                            }
                            if (excavatedBoundary.Contains((boundary.y + 1, boundary.x)))
                            {
                                previousInflectionShape = "D";
                            }
                        }
                    }

                    previousBoundary = boundary;
                }
            }
        }

        private void DrawExcavation()
        {
            int ExcavationWidth = excavatedBoundary.Max(hole => hole.x); // - holes.Min(hole => hole.x); if digger went negative on x axis we would need this extra logic.
            int ExcavationDepth = excavatedBoundary.Max(hole => hole.y);
            Console.WriteLine();

            for (int i = excavatedBoundary.Min(hole => hole.y) - 2; i <= ExcavationDepth; i++)
            {
                for (int j = excavatedBoundary.Min(hole => hole.x) - 2; j <= ExcavationWidth; j++)
                {
                    if (excavatedBoundary.Contains((i, j)))
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.Write("#");
                    }
                    else if (interiorHoles.Contains((i, j)))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("#");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(".");
                    }
                }
                
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }

        private void DrawFilling()
        {
            int ExcavationWidth = excavatedBoundary.Max(hole => hole.x); // - holes.Min(hole => hole.x); if digger went negative on x axis we would need this extra logic.
            int ExcavationDepth = excavatedBoundary.Max(hole => hole.y);
            Console.WriteLine();

            for (int i = 0; i <= ExcavationDepth; i++)
            {
                for (int j = 0; j <= ExcavationWidth; j++)
                {
                    if (interiorHoles.Contains((i, j)))
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("#");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(".");
                    }
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }

        private void ProcessDigInstruction(string instruction)
        {
            string direction = instruction.Split(" ")[0];
            int distance = int.Parse(instruction.Split(" ")[1]);

            this.Excavate(direction, distance);

            // Console.WriteLine($"digger moved to {diggerLocation.y}, {diggerLocation.x}");
        }

        private void Excavate(string direction, int distance)
        {
            switch (direction)
            {
                case "R":
                    for (int i = 1; i <= distance; i++)
                    {
                        excavatedBoundary.Add((diggerLocation.y, diggerLocation.x + i));
                    }

                    diggerLocation = (diggerLocation.y, diggerLocation.x + distance);

                    break;
                case "L":
                    for (int i = 1; i <= distance; i++)
                    {
                        excavatedBoundary.Add((diggerLocation.y, diggerLocation.x - i));
                    }

                    diggerLocation = (diggerLocation.y, diggerLocation.x - distance);

                    break;
                case "U":
                    for (int i = 1; i <= distance; i++)
                    {
                        excavatedBoundary.Add((diggerLocation.y - i, diggerLocation.x));
                    }
                    
                    diggerLocation = (diggerLocation.y - distance, diggerLocation.x);

                    break;
                case "D":
                    for (int i = 1; i <= distance; i++)
                    {
                        excavatedBoundary.Add((diggerLocation.y + i, diggerLocation.x));
                    }

                    diggerLocation = (diggerLocation.y + distance, diggerLocation.x);

                    break;
            }
        }

        public override string SolvePart2()
        {
            throw new NotImplementedException();
        }
    }
}
