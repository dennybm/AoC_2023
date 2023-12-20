using ConsoleApp1.Day16.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day17
{
    internal class Day17 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day17\\inputs.txt";
        int[,] grid;
        Dictionary<int, int>[,] minLossGrid;
        int gridWidth;
        int gridHeight;
        int minimumHeatLoss = 2000;
        Directions[] directions;

        public override string Solve()
        {
            CreateGrid();
            this.directions = (Directions[])Enum.GetValues(typeof(Directions));

            FindMinimumHeadLoss().ToString();

            Console.WriteLine($"Minimum heat loss was {minimumHeatLoss}");

            return minimumHeatLoss.ToString();
        }

        private int FindMinimumHeadLoss()
        {
            this.TakeStep(0, 0, Directions.Left, -1, 0, new List<(int row, int column, Directions direction, int runningCount)>());

            return minimumHeatLoss;
        }

        private int[,] CreateGrid()
        {
            int[,] _grid;

            string[] input = this.ReadAllFile(filePath);
            _grid = new int[input.Length, input[0].Length];
            minLossGrid = new Dictionary<int, int>[input.Length, input[0].Length];

            for (int rowIndex = 0; rowIndex < input.Length; rowIndex++)
            {
                string line = input[rowIndex];

                for (int columnIndex = 0; columnIndex < line.Length; columnIndex++)
                {
                    minLossGrid[rowIndex, columnIndex] = new Dictionary<int, int>()
                    {
                        { 0, int.MaxValue },
                        { 1, int.MaxValue },
                        { 2, int.MaxValue },
                    };

                    _grid[rowIndex, columnIndex] = int.Parse(line[columnIndex].ToString());
                }
            }

            this.grid = _grid;

            this.gridHeight = _grid.GetLength(0);
            this.gridWidth = _grid.GetLength(1);

            return _grid;
        }

        private void TakeStep(int rowIndex, int columnIndex, Directions fromDirection, int StepsInDirection, int heatLoss, List<(int row, int column, Directions direction, int runningCount)> path, int? depth = null)
        {
            if (depth == null)
            {
                depth = -1;
            }

            depth++;

            // return if not in boundary, or we have taken 3 steps in the drection.
            if (rowIndex < 0 || rowIndex > gridHeight - 1 || columnIndex < 0 || columnIndex > gridWidth - 1 || StepsInDirection > 2)
            {
                return;
            }

            // check if at end point and update the minimum heatloss if we are below it.
            if (columnIndex == gridWidth - 1 && rowIndex == gridHeight - 1)
            {
                // add the final heatloss
                heatLoss += grid[rowIndex, columnIndex];

                if (heatLoss < minimumHeatLoss)
                {

                    Console.WriteLine("New best path, heatloss: " + heatLoss);
                    minimumHeatLoss = heatLoss;

                    this.DrawPath(path);
                }

                return;
            }

            // If we get to a square and the count is higher than that square, and the steps taken is greater than that, then we can exit.
            switch (StepsInDirection)
            {
                case 0:

                    if (heatLoss + grid[rowIndex, columnIndex] > minLossGrid[rowIndex, columnIndex][0])
                    {
                        return;
                    }

                    for (int i = 2; i < 3; i++)
                    {
                        if (heatLoss + grid[rowIndex, columnIndex] < minLossGrid[rowIndex, columnIndex][i])
                        {
                            minLossGrid[rowIndex, columnIndex][i] = minimumHeatLoss + grid[rowIndex, columnIndex];
                        }
                    }
                    break;
                case 1:
                    if (heatLoss> minLossGrid[rowIndex, columnIndex][2])
                    {
                        return;
                    }
                    break;
                case 2:
                    for (int i = 0; i < 2; i++)
                    {
                        if (heatLoss + grid[rowIndex, columnIndex] > minLossGrid[rowIndex, columnIndex][i])
                        {
                            return;
                        }
                    }

                    minLossGrid[rowIndex, columnIndex][2] = minimumHeatLoss + grid[rowIndex, columnIndex];
                    break;
            }

            // directions = this.OrderDirections(rowIndex, columnIndex);

            // otherwise take a step in each of the directions.
            foreach (Directions direction in directions.Except(new Directions[] { fromDirection }))
            {
                switch (direction)
                {
                    case Directions.Down:
                        {
                            int _stepsInDirection = 0;

                            if (fromDirection == Directions.Up)
                            {
                                _stepsInDirection = StepsInDirection + 1;
                            }

                            if (_stepsInDirection > 2)
                            {
                                break;
                            }

                            TakeStep(rowIndex + 1, columnIndex, Directions.Up, _stepsInDirection, heatLoss + grid[rowIndex, columnIndex], path.Append((rowIndex, columnIndex, direction, heatLoss + grid[rowIndex, columnIndex])).ToList(), depth);
                            break;
                        }

                    case Directions.Right:
                        {

                            if (depth == 0 || depth == 1)
                            {
                                Console.WriteLine();
                            }
                            int _stepsInDirection = 0;

                            if (fromDirection == Directions.Left)
                            {
                                _stepsInDirection = StepsInDirection + 1;
                            }
                            else
                            {
                                _stepsInDirection = 0;
                            }

                            if (_stepsInDirection > 2)
                            {
                                break;
                            }

                            TakeStep(rowIndex, columnIndex + 1, Directions.Left, _stepsInDirection, heatLoss + grid[rowIndex, columnIndex], path.Append((rowIndex, columnIndex, direction, heatLoss + grid[rowIndex, columnIndex])).ToList(), depth);
                            break;
                        }

                    case Directions.Up:
                        {
                            int _stepsInDirection = 0;

                            if (fromDirection == Directions.Down)
                            {
                                _stepsInDirection = StepsInDirection + 1;
                            }
                            else
                            {
                                _stepsInDirection = 0;
                            }

                            if (_stepsInDirection > 2)
                            {
                                break;
                            }

                            TakeStep(rowIndex - 1, columnIndex, Directions.Down, _stepsInDirection, heatLoss + grid[rowIndex, columnIndex], path.Append((rowIndex, columnIndex, direction, heatLoss + grid[rowIndex, columnIndex])).ToList(), depth);
                            break;
                        }

                    case Directions.Left:
                        {
                            int _stepsInDirection = 0;

                            if (fromDirection == Directions.Right)
                            {
                                _stepsInDirection = StepsInDirection + 1;
                            }
                            else
                            {
                                _stepsInDirection = 0;
                            }

                            if (_stepsInDirection > 2)
                            {
                                break;
                            }

                            TakeStep(rowIndex, columnIndex - 1, Directions.Right, _stepsInDirection, heatLoss + grid[rowIndex, columnIndex], path.Append((rowIndex, columnIndex, direction, heatLoss + grid[rowIndex, columnIndex])).ToList(), depth);
                            break;
                        }
                }
            }
        }

        private void DrawPath(List<(int row, int column, Directions direction, int runningCount)> path)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
            for (int rowIndex = 0; rowIndex < gridHeight; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < gridWidth; columnIndex++)
                {
                    bool isInPath = path.Select(x => (x.row, x.column)).ToList().Contains((rowIndex, columnIndex));

                    Console.BackgroundColor = isInPath ? ConsoleColor.Yellow : ConsoleColor.Black;
                    Console.ForegroundColor = isInPath ? ConsoleColor.Black : ConsoleColor.Gray;
                    Console.Write(grid[rowIndex, columnIndex]);
                }

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();
            }
        }

        private Directions[] OrderDirections(int rowIndex, int columnIndex)
        {
            Directions[] orderedDirections = new Directions[4];

            if (gridWidth - columnIndex > gridHeight - rowIndex)
            {
                orderedDirections = new Directions[] { Directions.Right, Directions.Down, Directions.Up, Directions.Left };
            }
            else
            {
                orderedDirections = new Directions[] { Directions.Down, Directions.Right, Directions.Up, Directions.Left };
            }

            return orderedDirections;
        }

        public override string SolvePart2()
        {
            throw new NotImplementedException();
        }
    }
}
