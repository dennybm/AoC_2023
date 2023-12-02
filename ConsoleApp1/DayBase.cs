using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal abstract class DayBase
    {
        public string GetSolution()
        {
            string result = Solve();
            return result;
        }

        public abstract string Solve();
    }
}
