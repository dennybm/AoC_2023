using ConsoleApp1.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day10
{
    internal partial class Day10 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day10\\inputs.txt";

        public override string Solve()
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

            int[] firstPipeCoords = new int[2];
            int[] lastDirection = new int[2];

            int[] above = sCoords.AddArray(MazeUtils.Up);
            int[] below = sCoords.AddArray(MazeUtils.Down);
            int[] right = sCoords.AddArray(MazeUtils.Right);
            int[] left = sCoords.AddArray(MazeUtils.Left);

            if (maze.GetPipe(above).IsAboveConnected())
            {
                lastDirection = MazeUtils.Up;
                firstPipeCoords = above;
            }
            else if (maze.GetPipe(below).IsBelowConnected())
            {
                lastDirection = MazeUtils.Down;
                firstPipeCoords = below;
            }
            else if (maze.GetPipe(right).IsRightConnected())
            {
                lastDirection = MazeUtils.Right;
                firstPipeCoords = right;
            }
            else if (maze.GetPipe(left).IsLeftConnected())
            {
                lastDirection = MazeUtils.Left;
                firstPipeCoords = left;
            }

            int[] location = firstPipeCoords;
            int[] nextDirection = new int[2];
            int loopLength = 1;
            bool loopIncomplete = true;

            while (loopIncomplete)
            {
                loopLength++;

                switch (maze.GetPipe(location))
                {
                    case '|':
                        nextDirection = lastDirection == MazeUtils.Up ? MazeUtils.Up : MazeUtils.Down;
                        location = location.AddArray(nextDirection);
                        lastDirection = nextDirection;
                        break;
                    case '-':
                        nextDirection = lastDirection == MazeUtils.Left ? MazeUtils.Left : MazeUtils.Right;
                        location = location.AddArray(nextDirection);
                        lastDirection = nextDirection; break;
                    case 'L':
                        nextDirection = lastDirection == MazeUtils.Down ? MazeUtils.Right : MazeUtils.Up;
                        location = location.AddArray(nextDirection);
                        lastDirection = nextDirection; break;
                    case 'J':
                        nextDirection = lastDirection == MazeUtils.Down ? MazeUtils.Left : MazeUtils.Up;
                        location = location.AddArray(nextDirection);
                        lastDirection = nextDirection;
                        break;
                    case '7':
                        nextDirection = lastDirection == MazeUtils.Up ? MazeUtils.Left : MazeUtils.Down;
                        location = location.AddArray(nextDirection);
                        lastDirection = nextDirection; break;
                    case 'F':
                        nextDirection = lastDirection == MazeUtils.Up ? MazeUtils.Right : MazeUtils.Down;
                        location = location.AddArray(nextDirection);
                        lastDirection = nextDirection;
                        break;
                    case 'S':
                        loopIncomplete = false;
                        break;
                }
            }

            result = (loopLength / 2).ToString();

            return result;
        }

    }
}
