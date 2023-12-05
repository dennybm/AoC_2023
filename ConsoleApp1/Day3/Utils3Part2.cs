using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day3
{
    internal static partial class Utils3
    {
        public static int GetGearNumberSum(string[] engineSchematic)
        {
            int sum = 0;

            for (int lineIndex = 0; lineIndex < engineSchematic.Length; lineIndex++)
            {
                for (int charIndex = 0; charIndex < engineSchematic[lineIndex].Length; charIndex++)
                {
                    if (engineSchematic[lineIndex][charIndex] == '*')
                    {
                        List<int> adjacentParts = new List<int>();

                        // 1 2 3
                        // 4 * 5
                        // 6 7 8

                        // check position 1 for number
                        if (lineIndex > 0 && charIndex > 0 &&  char.IsNumber(engineSchematic[lineIndex - 1][charIndex - 1]))
                        {
                            (int partNumber, int tailLength, int headLength)  = GetAdjacentNumber(engineSchematic, lineIndex - 1, charIndex - 1);
                            adjacentParts.Add(partNumber);

                            // if the head length is less than 2, there could be a seperate number starting at position 3.
                            if (charIndex + 1 < engineSchematic[lineIndex - 1].Length && headLength < 2)
                            {
                                if (char.IsNumber(engineSchematic[lineIndex - 1][charIndex + 1]))
                                {
                                    (int topRightPart, int tailLengthTR, int headLengthTR)  = GetAdjacentNumber(engineSchematic, lineIndex - 1, charIndex + 1);
                                    adjacentParts.Add(topRightPart);
                                }
                            }
                        }
                        // if position 1 isn't a number, check position 2.
                        else if (lineIndex > 0 && char.IsNumber(engineSchematic[lineIndex - 1][charIndex]))
                        {
                            (int partNumber, int tailLength, int headLength)  = GetAdjacentNumber(engineSchematic, lineIndex - 1, charIndex);
                            adjacentParts.Add(partNumber);
                            // dont't check position 3 here as it would be part of the number in position 2.
                        }
                        // check position 3 if 1 and 2 were not numbers.
                        else if (lineIndex > 0 && charIndex + 1 < engineSchematic[lineIndex - 1].Length && char.IsNumber(engineSchematic[lineIndex - 1][charIndex + 1]))
                        {
                            (int partNumber, int tailLength, int headLength) = GetAdjacentNumber(engineSchematic, lineIndex - 1, charIndex + 1);
                            adjacentParts.Add(partNumber);
                        }

                        // check position 4
                        if (charIndex > 0 && char.IsNumber(engineSchematic[lineIndex][charIndex - 1]))
                        {
                            (int partNumber, int tailLength, int headLength) = GetAdjacentNumber(engineSchematic, lineIndex, charIndex - 1);
                            adjacentParts.Add(partNumber);
                        }

                        // check position 5
                        if (charIndex + 1 < engineSchematic[lineIndex].Length && char.IsNumber(engineSchematic[lineIndex][charIndex + 1]))
                        {
                            (int partNumber, int tailLength, int headLength) = GetAdjacentNumber(engineSchematic, lineIndex, charIndex + 1);
                            adjacentParts.Add(partNumber);
                        }

                        // check position 6 for number
                        if (lineIndex + 1 < engineSchematic.Length && charIndex > 0 && char.IsNumber(engineSchematic[lineIndex + 1][charIndex - 1]))
                        {
                            (int partNumber, int tailLength, int headLength) = GetAdjacentNumber(engineSchematic, lineIndex + 1, charIndex - 1);
                            adjacentParts.Add(partNumber);

                            // if the head length is less than 2, there could be a seperate number starting at position 8.
                            if (charIndex + 1 < engineSchematic[lineIndex + 1].Length && headLength < 2)
                            {
                                if (char.IsNumber(engineSchematic[lineIndex + 1][charIndex + 1]))
                                {
                                    (int topRightPart, int tailLengthTR, int headLengthTR) = GetAdjacentNumber(engineSchematic, lineIndex + 1, charIndex + 1);
                                    adjacentParts.Add(topRightPart);
                                }
                            }
                        }
                        // if position 6 isn't a number, check position 7.
                        else if (lineIndex + 1 < engineSchematic.Length && char.IsNumber(engineSchematic[lineIndex + 1][charIndex]))
                        {
                            (int partNumber, int tailLength, int headLength) = GetAdjacentNumber(engineSchematic, lineIndex + 1, charIndex);
                            adjacentParts.Add(partNumber);
                            // dont't check position 8 because it would be part of the number in position 7.
                        }
                        // check position 8 if 6 and 7 were not numbers.
                        else if (lineIndex + 1 < engineSchematic.Length && charIndex + 1 < engineSchematic[lineIndex + 1].Length && char.IsNumber(engineSchematic[lineIndex + 1][charIndex + 1]))
                        {
                            (int partNumber, int tailLength, int headLength) = GetAdjacentNumber(engineSchematic, lineIndex + 1, charIndex + 1);
                            adjacentParts.Add(partNumber);
                        }
                    
                        if (adjacentParts.Count == 2)
                        {
                            sum += (adjacentParts[0] * adjacentParts[1]);

                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write("*");

                            //Console.WriteLine($"Gear found with adjacent numbers {adjacentParts[0]} and {adjacentParts[1]}");

                            //if (lineIndex > 0)
                            //    Console.WriteLine(engineSchematic[lineIndex-1]);

                            //Console.WriteLine(engineSchematic[lineIndex ]);

                            //if (lineIndex + 1 < engineSchematic.Length)
                            //    Console.WriteLine(engineSchematic[lineIndex + 1]);
                        }
                        else if (adjacentParts.Count == 1)
                        {
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.Write("*");
                        }
                        else if (adjacentParts.Count > 2)
                        {
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("*");
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("*");
                        }

                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(engineSchematic[lineIndex][charIndex]);
                    }
                }

                Console.WriteLine();
            }

            return sum;
        }

        public static (int partNumber,int tailLength, int headLength) GetAdjacentNumber(string[] engineSchematic, int lineIndex, int charIndex)
        {
            string line = engineSchematic[lineIndex];
            string partNumber = line[charIndex].ToString();

            int headLength = 1;

            // check forward
            while (charIndex + headLength < line.Length && Char.IsNumber(line[charIndex + headLength]))
            {
                partNumber += line[charIndex + headLength].ToString();
                headLength++;
            }

            int tailLength = 1;

            // check backward
            while (charIndex - tailLength >= 0 && Char.IsNumber(line[charIndex - tailLength]))
            {
                partNumber = line[charIndex - tailLength].ToString() + partNumber;
                tailLength++;
            }

            return (int.Parse(partNumber), tailLength, headLength);
        }
    }
}
