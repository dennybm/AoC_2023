using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.Day7.Models
{
    internal static class HandUtils
    {
        public static int GetScore(string cards)
        {
            int result = 0;

            // 0'th index is reserved for hand strength, next 5 are assigned card value. 
            int[] cardValues = new int[6];

            for (int i = 1; i < cards.Length + 1; i++)
            {
                char card = cards[i-1];
                switch (card)
                {
                    case 'A':
                        cardValues[i] = 12;
                        break;
                    case 'K':
                        cardValues[i] = 11;
                        break;
                    case 'Q':
                        cardValues[i] = 10;
                        break;
                    case 'J':
                        cardValues[i] = 9;
                        break;
                    case 'T':
                        cardValues[i] = 8;
                        break;
                    default:
                        cardValues[i] = int.Parse(card.ToString()) - 2;
                        break;
                }
            }
            cardValues[0] = GetHandCategory(cards);

            // Convert to base 13.
            // [ 0, 1, 2, 3, 4, 5]
            for (int i = 0;  i < cardValues.Length; i++)
            {
                result += cardValues[i] * (int)Math.Pow(13, cardValues.Length - i - 1);
            }

            Console.WriteLine($"Hand {cards} has score: {result}");

            return result;
        }

        public static int GetHandCategory(string cards)
        {
            int result = 0;

            var groups = cards.GroupBy(c => c);

            int GroupCount = groups.Count();

            switch (GroupCount)
            {
                // all cards the same, 5 of a kind
                case 1:
                    result = 6;
                    break;

                // two groups of cards, either full house or four of a kind
                case 2:
                    int firstGroupCount = groups.First().Count();
                    result = firstGroupCount == 1 || firstGroupCount == 4 ? 5 : 4;
                    break;

                // three groups of cards, either two pair or three of a kind
                case 3:
                    foreach (var group in groups)
                    {
                        if (group.Count() == 3)
                        {
                            result = 3;
                            break;
                        }
                    }
                    if (result == 0)
                        result = 2; break;
                
                // four groups of cards, there is one pair.
                case 4:
                    result = 1;
                    break;

                default:
                    break;
            }

            Console.WriteLine($"Hand {cards} has category: {result}");

            return result;
        }
    }
}
