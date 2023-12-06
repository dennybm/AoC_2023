using ConsoleApp1.Day5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal static class StringUtils
    {
        public static int[] ToNonZeroNumberArray(this string str, string seperator = ",")
        {
            int[] result = str.Split(seperator)
                            .Where(seed => !string.IsNullOrEmpty(seed)).ToList()
                            .Select(int.Parse).ToArray();

            return result;
        }

        public static long[] ToNonZeroLongArray(this string str, string seperator = ",")
        {
            long[] result = str.Split(seperator)
                            .Where(seed => !string.IsNullOrEmpty(seed)).ToList()
                            .Select(long.Parse).ToArray();

            return result;
        }
    }
}
