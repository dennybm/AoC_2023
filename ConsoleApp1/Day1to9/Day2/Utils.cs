using ConsoleApp1.Day2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day2
{
    internal static class Utils
    {
        public static Game ConvertStringToGame(string line)
        {
            Game game = new Game();

            string[] colonSeperated = line.Split(':');
            game.Id = int.Parse(colonSeperated[0].Split(" ")[1]);

            game.AllDraws = new List<int[]>();

            string[] drawsString = colonSeperated[1].Split(";");

            // drawString example: 3 green, 4 blue, 1 red
            foreach (string drawString in drawsString)
            {
                int[] draw = new int[3];

                string[] colorsDrawn = drawString.Split(",");

                // colorsDrawn example: 3 green
                foreach (string colorString in colorsDrawn)
                {
                    string[] colorDrawArray = colorString.Split(" ");

                    string color = colorDrawArray[2];

                    switch (color)
                    {
                        case "red":
                            draw[0] = int.Parse(colorDrawArray[1]);
                            break;
                        case "green":
                            draw[1] = int.Parse(colorDrawArray[1]);
                            break;
                        case "blue":
                            draw[2] = int.Parse(colorDrawArray[1]);
                            break;
                        default:
                            break;
                    }
                }

                game.AllDraws.Add(draw);
            }

            return game;
        }

        public static bool CheckGamePossible(Game game, int[] parameters)
        {
            bool result = true;

            foreach (int[] draw in game.AllDraws)
            {
                bool drawPossible = draw[0] <= parameters[0] && draw[1] <= parameters[1] && draw[2] <= parameters[2];

                if (!drawPossible)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public static int GetGamePower (Game game)
        {
            // the number of balls of each color required for a game, is the largest number of balls of that color that was drawn at any one time.
            int minimumRed = game.AllDraws.Select(draw => draw[0]).Max();
            int minimumGreen = game.AllDraws.Select(draw => draw[1]).Max();
            int minimumBlue = game.AllDraws.Select(draw => draw[2]).Max();

            return minimumRed * minimumGreen * minimumBlue;
        }
    }
}
