using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day7.Models
{
    internal class Hand
    {
        public string Cards { get; set; } = string.Empty;

        public int Bid { get; set; }

        public int Score { get; set; }
    }
}
