using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day6.Models
{
    internal class Race
    {
        public long Time { get; set; }
        public long Distance { get; set; }

        public int GetRaceScore()
        {
            int result = 1;

            Console.WriteLine();
            Console.WriteLine($"Scoring race with time {Time}'ms and distance {Distance}'mm ");

            if (Math.Pow(this.Time / 2, 2) < this.Distance)
            {
                Console.WriteLine("Time cannot be beaten");

                result = 0;
            }
            else
            {
                // x*(t-x) = d
                // tx - x^2 = d
                // x^2 -tx + d = 0
                double[] intersections = Utils6.SolvePolynomial(1, -Time, Distance);

                if (intersections?.Length > 0 )
                {
                    int firstWinningTime = (int)(Math.Ceiling(intersections[0]));
                    int lastWinningTime = (int)Math.Floor(intersections[1]);

                    Console.WriteLine($"Winning times are from {firstWinningTime}, to {lastWinningTime}");

                    result = lastWinningTime - firstWinningTime + 1;

                    Console.WriteLine($"Score: {result}");
                    Console.WriteLine();
                }
            }
            
            return result;
        }
    }
}
