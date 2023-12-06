using ConsoleApp1.Day6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day6
{
    internal partial class Day6
    {
        public override string SolvePart2()
        {
            int result = 1;

            string[] data = this.ReadAllFile(filePath);
            List<Race> races = new List<Race>();
            long[] raceTimes = data[0].Split(':')[1]
                .ToNonZeroLongArray(" ");
            long[] raceDistances = data[1].Split(':')[1]
                .ToNonZeroLongArray(" ");

            string raceTimeString = string.Empty;
            string raceDistanceString = string.Empty;

            foreach(long raceTimeLong in raceTimes)
            {
                raceTimeString += raceTimeLong.ToString();
            }

            foreach (long raceDistanceLong in raceDistances)
            {
                raceDistanceString += raceDistanceLong.ToString();
            }

            long raceTime = long.Parse(raceTimeString);
            long raceDistance = long.Parse(raceDistanceString);

            Race race = new Race()
            {
                Time = raceTime,
                Distance = raceDistance,
            };


            return race.GetRaceScore().ToString();
        }
    }
}
