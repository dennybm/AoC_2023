using ConsoleApp1.Day7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day7
{
    internal partial class Day7 : DayBase
    {
        string filePath = "D:\\Projects\\AoC_2023\\AoC_2023\\ConsoleApp1\\Day7\\inputs.txt";
        List<Hand> hands = new List<Hand>();

        public override string Solve()
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
                        this.hands.Add(GetHandFromLine(line));
                    }
                }

                Console.WriteLine("File processing completed.");
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }

            List<Hand> orderedHands = hands.OrderBy(hand => hand.Score).ToList();

            for (int i = 0; i < orderedHands.Count; i++)
            {
                Hand hand = orderedHands[i];
                result += hand.Bid * (i + 1);

                Console.WriteLine($"Hand {hand.Cards} is rank {i} and has bid {hand.Bid}. New running total is {result} ");
            }

            return result.ToString();
        }

        private Hand GetHandFromLine(string line)
        {
            // line
            // ATAAA 513
            string[] lineSplit = line.Split(' ');

            Hand hand = new Hand 
            {
                Cards = lineSplit[0],
                Bid = int.Parse(lineSplit[1]),
            };

            return hand;
        }
    }
}
