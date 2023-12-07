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
        public static int GetScore(string cards, int part = 1)
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
                        if (part == 1)
                        {
                            cardValues[i] = 9;
                        }
                        else
                        {
                            cardValues[i] = 0;
                        }

                        break;
                    case 'T':
                        if (part == 1)
                        {
                            cardValues[i] = 8;
                        }
                        else
                        {
                            cardValues[i] = 9;
                        }
                        
                        break;
                    default:
                        if (part == 1)
                        {
                            cardValues[i] = int.Parse(card.ToString()) - 2;
                        }
                        else
                        {
                            cardValues[i] = int.Parse(card.ToString()) - 1;
                        }
                        break;
                }
            }

            if (part == 1)
            {
                cardValues[0] = GetHandCategory(cards);
            }
            else
            {
                cardValues[0] = GetHandCategoryPart2(cards);
            }

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

       /// <summary>
       /// Gets hand category.
       /// </summary>
       /// <param name="cards">Cards in the hand</param>
       /// <returns>
       /// 6: 5 of a kind
       /// 5: 4 of a kind
       /// 4: Full house
       /// 3: Three of a kind
       /// 2: Two pair
       /// 1: One pair
       /// 0: High card
       /// </returns>
        public static int GetHandCategoryPart2(string cards)
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

                    // in either case, if any card is a 'J' then the hand can become 5 of a kind.
                    if (cards.Contains('J'))
                    {
                        result = 6;
                    }
                    break;

                // three groups of cards, either two pair or three of a kind
                case 3:
                    foreach (var group in groups)
                    {
                        if (group.Count() == 3)
                        {
                            if (cards.Contains('J'))
                            {
                                // if a group of 3 is found, and the hand contains a J, then make four of a kind.
                                result = 5;
                            }
                            else
                            {
                                result = 3;
                            }

                            break;
                        }
                    }

                    if (result == 0)
                    {
                        // Two pairs
                        if (cards.Contains('J'))
                        {
                            if (groups.First(group => group.Key == 'J').Count() == 2)
                            {
                                // if J's are one of the pairs, make four of a kind
                                result = 5;
                            }
                            else
                            {
                                // J is not one of the pairs, make full house
                                result = 4;
                            }
                        }
                        else
                        {
                            result = 2;
                        }

                        break;
                    }
                    break;

                // four groups of cards, there is one pair.
                case 4:
                    result = 1;

                    if (cards.Contains('J'))
                    {
                        // make pair of J's another to get 3-o-a-k, or make a J the pair to make 3-o-a-k
                        result = 3;
                    }

                    break;

                default:
                    if (cards.Contains('J'))
                    {
                        result = 1;
                    }
                    break;
            }

            Console.WriteLine($"Hand {cards} has category: {result}");

            return result;
        }

    }
}
