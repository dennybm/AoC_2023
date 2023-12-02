using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day2.Models
{
    internal class Game
    {
        public int Id { get; set; }

        public List<int[]> AllDraws { get; set; }
    }
}
