using ConsoleApp1.Day16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day16
{
    internal partial class Day16 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day16\\inputs.txt";
        Sqaure[,] grid;

        public override string Solve()
        {
            CreateGrid();

            ShineLightAtGrid();

            return CountIlluminatedSquares().ToString();
        }

        private Sqaure[,] CreateGrid()
        {
            Sqaure[,] _grid;

            string[] input = this.ReadAllFile(filePath);
            _grid = new Sqaure[input.Length, input[0].Length];

            for (int rowIndex = 0; rowIndex < input.Length; rowIndex++)
            {
                string line = input[rowIndex];

                for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
                {
                    _grid[rowIndex, columnIndex] = new Sqaure() { Content = line[columnIndex] };
                }
            }

            this.grid = _grid;

            return _grid;
        }

        private int CountIlluminatedSquares()
        {
            int count = 0;

            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int columnIndex = 0;columnIndex < columns; columnIndex++)
                {
                    if (grid[rowIndex, columnIndex].IsIlluminated)
                    {
                        count++;
                    }
                }
            }

            //PrintGridAtEnd();

            return count;
        }

        private void PrintGrid()
        {
            //Console.BackgroundColor = ConsoleColor.Black;
            //Console.WriteLine();
            //int rows = grid.GetLength(0);
            //int columns = grid.GetLength(1);

            //for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            //{
            //    for (int columnIndex = 0; columnIndex < columns; columnIndex++)
            //    {
            //        if (grid[rowIndex, columnIndex].IsIlluminated)
            //        {
            //            Console.BackgroundColor = ConsoleColor.Yellow;
            //            Console.Write(grid[rowIndex, columnIndex].Content);
            //        }
            //        else
            //        {
            //            Console.BackgroundColor = ConsoleColor.Black;
            //            Console.Write(grid[rowIndex, columnIndex].Content);
            //        }
            //    }

            //    Console.BackgroundColor = ConsoleColor.Black;
            //    Console.WriteLine();
            //}
        }

        private void PrintGridAtEnd()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            int rows = grid.GetLength(0);
            int columns = grid.GetLength(1);

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < columns; columnIndex++)
                {
                    if (grid[rowIndex, columnIndex].IsIlluminated)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.Write(grid[rowIndex, columnIndex].Content);
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(grid[rowIndex, columnIndex].Content);
                    }
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }

        private void ShineLightAtGrid()
        {
            PrintGrid();
            ShineRight(0, -1);
        }

        private void ShineUp(int row, int column)
        {
            row--;

            while (row > -1)
            {
                grid[row, column].IsIlluminated = true;
                char content = grid[row, column].Content;

                if (grid[row, column].IsMirror && content != '|')
                {
                    if (grid[row, column].HitFromBottom)
                    {
                        break;
                    }
                    else
                    {
                        grid[row, column].HitFromBottom = true;

                        if (content == '/')
                        {
                            this.ShineRight(row, column);
                            break;
                        }
                        else if (content == '-')
                        {
                            this.ShineLeft(row, column);
                            this.ShineRight(row, column);
                            break;
                        }
                        else if (content == '\\')
                        {
                            this.ShineLeft(row, column);
                            break;
                        }
                    }
                }
                else
                {
                    row--;
                }
            }

            PrintGrid();
        }

        private void ShineRight(int row, int column)
        {
            column++;

            int columns = grid.GetLength(1);

            while (column < grid.GetLength(1))
            {
                grid[row, column].IsIlluminated = true;
                char content = grid[row, column].Content;

                if (grid[row, column].IsMirror && content != '-')
                {
                    if (grid[row, column].HitFromRight)
                    {
                        break;
                    }
                    else
                    {
                        grid[row, column].HitFromRight = true;

                        if (content == '/')
                        {
                            this.ShineUp(row, column);
                            break;
                }
                        else if (content == '|')
                        {
                            this.ShineUp(row, column);
                            this.ShineDown(row, column);
                            break;
                        }
                        else if (content == '\\')
                        {
                            this.ShineDown(row, column);
                            break;
                        }
                    }
                }
                else
                {
                    column++;
                }
            }

            PrintGrid();
        }
        private void ShineDown(int row, int column)
        {
            row++;

            while (row < grid.GetLength(0))
            {
                grid[row, column].IsIlluminated = true;
                char content = grid[row, column].Content;

                if (grid[row, column].IsMirror && content != '|')
                {
                    if (grid[row, column].HitFromTop)
                    {
                        break;
                    }
                    else
                    {
                        grid[row,column].HitFromTop = true;

                        if (content == '/')
                        {
                            this.ShineLeft(row, column);
                            break;
                        }
                        else if (content == '-')
                        {
                            this.ShineLeft(row, column);
                            this.ShineRight(row, column);
                            break;
                        }
                        else if (content == '\\')
                        {
                            this.ShineRight(row, column);
                            break;
                        }
                    }
                }
                else
                {
                    row++;
                }
            }

            PrintGrid();
        }
        private void ShineLeft(int row, int column)
        {
            column--;

            while (column > -1)
            {
                grid[row, column].IsIlluminated = true;
                char content = grid[row, column].Content;

                if (grid[row, column].IsMirror && content != '-')
                {
                    if (grid[row, column].HitFromLeft)
                    {
                        break;
                    }
                    else
                    {
                        grid[row, column].HitFromLeft = true;

                        if (content == '/')
                        {
                            this.ShineDown(row, column);
                            break;
                        }
                        else if (content == '|')
                        {
                            this.ShineUp(row, column);
                            this.ShineDown(row, column);
                            break;
                        }
                        else if (content == '\\')
                        {
                            this.ShineUp(row, column);
                            break;
                        }
                    }
                }
                else
                {
                    column--;
                }
            }

            PrintGrid();
        }

        public override string SolvePart2()
        {
            List<int> counts = new();
            grid = this.CreateGrid();

            // all rights
            for (int rowIndex = 0; rowIndex < grid.GetLength (0); rowIndex++)
            {
                ShineRight(rowIndex, -1);
                counts.Add(CountIlluminatedSquares());
                grid = this.CreateGrid();
            }

            // all lefts
            for (int rowIndex = 0; rowIndex < grid.GetLength(0); rowIndex++)
            {
                ShineLeft(rowIndex, grid.GetLength(1));
                counts.Add(CountIlluminatedSquares());
                grid = this.CreateGrid();
            }

            // all ups
            for (int columnIndex = 0; columnIndex < grid.GetLength(1); columnIndex++)
            {
                ShineUp(grid.GetLength(0), columnIndex);
                counts.Add(CountIlluminatedSquares());
                grid = this.CreateGrid();
            }

            // all downs
            for (int columnIndex = 0; columnIndex < grid.GetLength(1); columnIndex++)
            {
                ShineDown(-1, columnIndex);
                counts.Add(CountIlluminatedSquares());
                grid = this.CreateGrid();
            }

            return counts.Max().ToString();
        }
    }
}
