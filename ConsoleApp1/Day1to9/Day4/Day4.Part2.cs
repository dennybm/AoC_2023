using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day4
{
    internal partial class Day4
    {
        int[] copyCount = new int[] { 0 };
        int count = 0;

        public override string SolvePart2()
        {
            int result = 0;

            if (File.Exists(filePath))
            {
                // Open the file with a StreamReader
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine($"Processing line {++count} | Running total {result}");
                        
                        result += ProcessLinePart2(line);
                    }
                }

                Console.WriteLine("File processing completed.");
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }

            // If there are no copies for the last card add 1.
            if (copyCount.Length == 0)
                copyCount = new int[1] { 1 };

            // add on any remaining copies 
            foreach (int i in copyCount)
            {
                result += i;
            }

            return result.ToString();
        }

        // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        private int ProcessLinePart2(string line)
        {
            // how many copies of this card? (always incriment by one as we have an original copy)
            copyCount[0] = copyCount[0] + 1;
            int result = copyCount[0];

            Console.WriteLine($"There are {result - 1} copies of card {count}, and one original card.");

            string[] numbers = line.Split(':')[1].Split('|');

            string[] winners = numbers[0].Split(' ').Where(num => num != string.Empty).ToArray();
            string[] guesses = numbers[1].Split(" ").Where(num => num != string.Empty).ToArray();

            int matches = winners.Intersect(guesses).Count();

            Console.WriteLine($"Card {count} has {matches} matches");

            // we now generate "n" copies, of the next "m" cards, where n is the number of this card we have (copyCount[0]), and m is the number of matches on this card (matches).

            if (matches >= copyCount.Length)
            {
                // create array for new copies
                int[] newCopyCountArray = Enumerable.Repeat(copyCount[0], matches).ToArray();

                // add number of copies we had
                for (int i = 0; i < copyCount.Length - 1; i++)
                {
                    newCopyCountArray[i] = newCopyCountArray[i] + copyCount[i + 1];
                }

                copyCount = newCopyCountArray;
            }
            else
            {
                int[] newCopyCountArray = new int[copyCount.Length - 1];

                // add number of new copies to old array of copies
                for (int i = 0; i < copyCount.Length - 1; i++)
                {
                    newCopyCountArray[i] = copyCount[i + 1];

                    if (i < matches)
                    {
                        newCopyCountArray[i] += copyCount[0];
                    }
                }

                copyCount = newCopyCountArray;
            }

            Console.Write("New Copy array: ");

            foreach (int i in copyCount)
            {
                Console.Write(i + ", ");
            }
            
            Console.WriteLine();

            // If there are no copies left reset to 0.
            if (copyCount.Length == 0)
                copyCount = new int[1];

            return result;
        }
    }
}
