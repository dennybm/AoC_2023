using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day1
{
    internal class Day1 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day1\\inputs.txt";

        public override string Solve()
        {
            int result = 0;
            int count = 0;

            if (File.Exists(filePath))
            {
                // Open the file with a StreamReader
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        result += GetLineValue(line);

                        Console.WriteLine($"lines checked {++count} | Running total {result}");
                    }
                }

                Console.WriteLine("File processing completed.");
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }

            return result.ToString();
        }

        public override string SolvePart2()
        {
            int result = 0;
            int count = 0;

            if (File.Exists(filePath))
            {
                // Open the file with a StreamReader
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        result += GetLineValuePart2(line);

                        Console.WriteLine($"lines checked {++count} | Running total {result}");
                    }
                }

                Console.WriteLine("File processing completed.");
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }

            return result.ToString();
        }

        private int GetLineValue(string line)
        {
            string resultString = string.Empty;

            string numbers = new string(line.Where(Char.IsDigit).ToArray());

            if (numbers == null)
                resultString = "0";
            else if (numbers.Count() == 1)
                resultString = numbers.First().ToString() + numbers.First().ToString();
            else
                resultString = numbers.First().ToString() + numbers.Last().ToString();

            Console.WriteLine($"result {resultString} | {line}");

            return int.Parse(resultString);
        }

        private int GetLineValuePart2(string line)
        {
            string resultString = string.Empty;
            string firstNumber = string.Empty;
            string lastnumber = string.Empty;

            Dictionary<string, string> numberDict = new Dictionary<string, string>()
            {
                {"one", "1" },
                {"two", "2" },
                {"three", "3" },
                {"four", "4" },
                {"five", "5" },
                {"six", "6" },
                {"seven", "7" },
                {"eight", "8" },
                {"nine", "9" },
                {"zero", "0" },
            };

            // Check each character, if it's a number take that value, if it's a letter, check to see if it spells out a number:
            for (int j = 0; j < line.Length; j++)
            {
                if (Char.IsNumber(line[j]))
                {
                    firstNumber = line[j].ToString();
                    break;
                }
                else
                {
                    for (int i = 1; i < 6 && j + i < line.Length; i++)
                    {
                        string possibleNumber = line.Substring(j, i);

                        if (numberDict.ContainsKey(possibleNumber))
                        {
                            firstNumber = numberDict[possibleNumber];
                            break;
                        }
                    }

                    if (firstNumber != string.Empty)
                        break;
                }
            }

            // Same as above but backwards.
            for (int j = line.Length - 1; j > -1; j--)
            {
                if (Char.IsNumber(line[j]))
                {
                    lastnumber = line[j].ToString();
                    break;
                }
                else
                {
                    for (int i = 1; i < 6 && j - i > -1 ; i++)
                    {
                        string possibleNumber = line.Substring(j - i + 1, i);

                        if (numberDict.ContainsKey(possibleNumber))
                        {
                            lastnumber = numberDict[possibleNumber];
                            break;
                        }
                    }

                    if (lastnumber != string.Empty)
                        break;
                }
            }

            resultString = firstNumber + lastnumber;

            Console.WriteLine($"result {resultString} | {line}");

            return int.Parse(resultString);
        }
    }
}