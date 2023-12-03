using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day3
{
    internal static class Utils3
    {
        public static int GetPartNumberSum(string[] engineSchematic)
        {
            int sum = 0;

            for(int lineIndex = 0;  lineIndex < engineSchematic.Length; lineIndex++)
            {
                sum += EvaluateLine(engineSchematic[lineIndex], lineIndex, engineSchematic);
            }

            return sum;
        }

        private static int EvaluateLine(string line, int lineNumber, string[] engineSchematic)
        {
            int sum = 0;

            for (int charIndex = 0; charIndex < line.Length; charIndex++)
            {
                if (Char.IsNumber(line[charIndex]))
                {
                    // Get part number
                    string partNumber = line[charIndex].ToString();

                    int numberLength = 1;
                    while (charIndex + numberLength < line.Length && Char.IsNumber(line[charIndex + numberLength]))
                    {
                        partNumber += line[charIndex + numberLength].ToString();
                        numberLength++;
                    }

                    if (IsAdjacentToSymbol(line, lineNumber, engineSchematic, charIndex, partNumber))
                    {
                        Console.WriteLine($"Found {partNumber} in {line} | add to total");
                        sum += int.Parse(partNumber);

                    }
                    else
                    {
                        Console.WriteLine($"Number not adjacent to symbol: {partNumber} in {line} | do not add to total");
                    }

                    // Skip to next unchecked character.
                    charIndex += numberLength;
                }
            }

            return sum;
        }

        private static bool IsAdjacentToSymbol(string line, int lineNumber, string[] engineSchematic, int partIndex, string partNumber)
        {
            bool result = false;

            // check above
            if (lineNumber != 0)
            {
                for(int i = 0; i < partNumber.Length; i++)
                {
                    if (IsSymbol(engineSchematic[lineNumber - 1][partIndex + i]))
                    {
                        return true;
                    }
                }

                // check above corners
                if (partIndex!= 0)
                {
                    if (IsSymbol(engineSchematic[lineNumber - 1][partIndex - 1]))
                    {
                        return true;
                    }
                }

                if (partIndex + partNumber.Length < line.Length - 1)
                {
                    if (IsSymbol(engineSchematic[lineNumber - 1][partIndex + partNumber.Length]))
                    {
                        return true;
                    }
                }
            }

            // Check below
            if (lineNumber + 1 < engineSchematic.Length)
            {
                for (int i = 0; i < partNumber.Length; i++)
                {
                    if (IsSymbol(engineSchematic[lineNumber + 1][partIndex + i]))
                    {
                        return true;
                    }
                }

                // check below corners
                if (partIndex != 0)
                {
                    if (IsSymbol(engineSchematic[lineNumber + 1][partIndex - 1]))
                    {
                        return true;
                    }
                }

                if (partIndex + partNumber.Length < line.Length - 1)
                {
                    if (IsSymbol(engineSchematic[lineNumber + 1][partIndex + partNumber.Length]))
                    {
                        return true;
                    }
                }
            }

            // Check left and right
            if (partIndex != 0)
            {
                if (IsSymbol(engineSchematic[lineNumber][partIndex - 1]))
                {
                    return true;
                }
            }

            if (partIndex + partNumber.Length < line.Length - 1)
            {
                if (IsSymbol(engineSchematic[lineNumber][partIndex + partNumber.Length]))
                {
                    return true;
                }
            }

            return result;
        }

        private static bool IsSymbol(char c)
        {
            return c != '.' && !Char.IsNumber(c);
        }
    }
}
