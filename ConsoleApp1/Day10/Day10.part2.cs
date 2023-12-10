using ConsoleApp1.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day10
{
    internal partial class Day10
    {
        public override string SolvePart2()
        {
            string result = string.Empty;

            string[] maze = this.ReadAllFile(filePath);

            var sCoords = new int[2];

            for (int y = 0; y < maze.Length; y++)
            {
                if (maze[y].Contains('S'))
                {
                    sCoords[0] = maze[y].IndexOf('S');
                    sCoords[1] = y;
                    break;
                }
            }

            int[] firstPipe = new int[2];
            int[] previousDirection = new int[2];

            int[] above = sCoords.AddArray(MazeUtils.Up);
            int[] below = sCoords.AddArray(MazeUtils.Down);
            int[] right = sCoords.AddArray(MazeUtils.Right);
            int[] left = sCoords.AddArray(MazeUtils.Left);

            if (maze.GetPipe(above).IsAboveConnected())
            {
                previousDirection = MazeUtils.Up;
                firstPipe = above;
            }
            else if (maze.GetPipe(below).IsBelowConnected())
            {
                previousDirection = MazeUtils.Down;
                firstPipe = below;
            }
            else if (maze.GetPipe(right).IsRightConnected())
            {
                previousDirection = MazeUtils.Right;
                firstPipe = right;
            }
            else if (maze.GetPipe(left).IsLeftConnected())
            {
                previousDirection = MazeUtils.Left;
                firstPipe = left;
            }

            int[] location = firstPipe;
            int[] nextDirection = new int[2];
            List<int[]> loopCoords = new List<int[]> { sCoords, firstPipe };
            List<int[]> loopDirection = new List<int[]> {};
            int loopLength = 1;
            bool loopIncomplete = true;

            while (loopIncomplete)
            {
                loopLength++;

                switch (maze.GetPipe(location))
                {
                    case '|':
                        nextDirection = previousDirection == MazeUtils.Up ? MazeUtils.Up : MazeUtils.Down;
                        location = location.AddArray(nextDirection);
                        previousDirection = nextDirection;
                        break;
                    case '-':
                        nextDirection = previousDirection == MazeUtils.Left ? MazeUtils.Left : MazeUtils.Right;
                        location = location.AddArray(nextDirection);
                        previousDirection = nextDirection; break;
                    case 'L':
                        nextDirection = previousDirection == MazeUtils.Down ? MazeUtils.Right : MazeUtils.Up;
                        location = location.AddArray(nextDirection);
                        previousDirection = nextDirection; break;
                    case 'J':
                        nextDirection = previousDirection == MazeUtils.Down ? MazeUtils.Left : MazeUtils.Up;
                        location = location.AddArray(nextDirection);
                        previousDirection = nextDirection;
                        break;
                    case '7':
                        nextDirection = previousDirection == MazeUtils.Up ? MazeUtils.Left : MazeUtils.Down;
                        location = location.AddArray(nextDirection);
                        previousDirection = nextDirection; break;
                    case 'F':
                        nextDirection = previousDirection == MazeUtils.Up ? MazeUtils.Right : MazeUtils.Down;
                        location = location.AddArray(nextDirection);
                        previousDirection = nextDirection;
                        break;
                    case 'S':
                        loopIncomplete = false;
                        break;
                }

                loopDirection.Add(previousDirection);
                loopCoords.Add(location);
            }

            List<int[]> insideBorderCoords = this.GetBorderCoords(maze, loopCoords, loopDirection);

            maze.WriteMazeToConsole(loopCoords, insideBorderCoords);

            result = (loopLength / 2).ToString();

            return result;
        }

        private List<int[]> GetBorderCoords(string[] maze, List<int[]> loopCoords, List<int[]> loopDirections)
        {
            List<int[]> result = new List<int[]>();

            for (int loopIndex = 2; loopIndex < loopCoords.Count - 1; loopIndex++)
            {
                int[]? loopCoord = loopCoords[loopIndex];
                char pipe = maze.GetPipe(loopCoords[loopIndex]);

                var previousDirection = loopDirections[loopIndex - 2];

                Console.WriteLine("Pipe " + pipe.ToString());
                Console.WriteLine("Previous direction: " + previousDirection[0] + ", " + previousDirection[1]);

                switch (pipe)
                {
                    case '|':
                        if (previousDirection.ArraysEqual(MazeUtils.Down))
                        {
                            result.Add(new int[] { loopCoord[0] - 1, loopCoord[1] });
                        }
                        else
                        {
                            result.Add(new int[] { loopCoord[0] + 1, loopCoord[1] });
                        }

                        break;
                    case '-':
                        if (previousDirection.ArraysEqual(MazeUtils.Right))
                        {
                            result.Add(new int[] { loopCoord[0], loopCoord[1] + 1 });
                        }
                        else
                        {
                            result.Add(new int[] { loopCoord[0], loopCoord[1] - 1 });
                        }

                        break;
                    case 'L':
                        if (previousDirection.ArraysEqual(MazeUtils.Down))
                        {
                            result.Add(new int[] { loopCoord[0] - 1, loopCoord[1] });
                            result.Add(new int[] { loopCoord[0], loopCoord[1] + 1 });
                        }
                        else
                        {
                        }

                        break;
                    case 'J':
                        if (previousDirection.ArraysEqual(MazeUtils.Right))
                        {
                            result.Add(new int[] { loopCoord[0] + 1, loopCoord[1] });
                            result.Add(new int[] { loopCoord[0], loopCoord[1] + 1 });
                        }
                        else
                        {
                        }

                        break;

                    case '7':
                        if (previousDirection.ArraysEqual(MazeUtils.Up))
                        {
                            result.Add(new int[] { loopCoord[0], loopCoord[1] - 1 });
                            result.Add(new int[] { loopCoord[0] + 1, loopCoord[1] });
                        }
                        else
                        {
                        }

                        break;

                    case 'F':
                        if (previousDirection.ArraysEqual(MazeUtils.Left))
                        {
                            result.Add(new int[] { loopCoord[0], loopCoord[1] - 1 });
                            result.Add(new int[] { loopCoord[0] - 1, loopCoord[1] });
                        }
                        else
                        {
                        }

                        break;
                    case 'S':
                        if (previousDirection.ArraysEqual(MazeUtils.Down))
                        {
                            result.Add(new int[] { loopCoord[0], loopCoord[1] });
                        }
                        else
                        {
                            result.Add(new int[] { loopCoord[0], loopCoord[1] });
                        }

                        break;
                }
            }

            List<int[]> bordersNotInLoop = result.Where(borderCoord => !loopCoords.ContainsCoord(borderCoord)).ToList();
            List<int[]> borderFill = new List<int[]>();

            foreach (int[] border in bordersNotInLoop)
            {
                // check if next square is in the loop
                int i = 1;
                int[] nextCoord = { border[0] + 1, border[1] };

                while (!loopCoords.ContainsCoord(nextCoord) && nextCoord[0] < maze[0].Length)
                {
                    borderFill.Add(nextCoord);

                    nextCoord = new int[] { nextCoord[0] + 1, nextCoord[1] };
                }
            }

            bordersNotInLoop.AddRange(borderFill);

            return bordersNotInLoop;
        }
    }
}
