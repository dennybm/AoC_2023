using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day10
{
    internal static class MazeUtils
    {
        public static int[] Up = new int[] { 0, -1 };
        public static int[] Down = new int[] { 0, 1 };
        public static int[] Left = new int[] { -1, 0 };
        public static int[] Right = new int[] { 1, 0 };

        public static bool IsAboveConnected(this char pipe)
        {
            return pipe == '|' || pipe == '7' || pipe == 'F';
        }

        public static bool IsBelowConnected(this char pipe)
        {
            return pipe == '|' || pipe == 'L' || pipe == 'J';
        }

        public static bool IsRightConnected(this char pipe)
        {
            return pipe == '-' || pipe == '7' || pipe == 'J';
        }

        public static bool IsLeftConnected(this char pipe)
        {
            return pipe == '-' || pipe == 'L' || pipe == 'F';
        }

        public static char GetPipe(this string[] maze, int[] coords)
        {
            return maze[coords[1]][coords[0]];
        }

        public static void WriteMazeToConsole(this string[] maze, List<int[]> loopCoords, List<int[]> insideBorderCoords)
        {
            int result = 0;

            for (int rowIndex = 0; rowIndex < maze.Length; rowIndex++)
            {
                string row = maze[rowIndex];
                for (int columnIndex = 0; columnIndex < row.Length; columnIndex++)
                {
                    ConsoleColor color = ConsoleColor.Black;

                    color = loopCoords.ContainsCoord(new int[] {columnIndex, rowIndex}) ? ConsoleColor.Red : ConsoleColor.White;
                    color = insideBorderCoords.ContainsCoord(new int[] { columnIndex, rowIndex }) ? ConsoleColor.Yellow: color;

                    Console.BackgroundColor = color;

                    if (color == ConsoleColor.Magenta)
                    {
                        result++;
                    }

                    if (maze[rowIndex][columnIndex] == 'S')
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }

                    Console.Write(maze[rowIndex][columnIndex]);
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }

            Console.WriteLine($"Total fill squares is {result}");
        }

        public static bool ContainsCoord(this List<int[]> list, int[] coord)
        {
            foreach (int[] item in  list)
            {
                if (item[0] == coord[0] && item[1] == coord[1])
                {
                    return true;
                }
            }

            return false;
        }
    }
}
