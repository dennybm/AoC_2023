using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day9
{
    internal static class ArrayExtensions
    {
        public static IEnumerable<T> Prepend<T>(this T[] array, T value)
        {
            T[] result = new T[array.Count() + 1];
            result[0] = value;
            for (int i = 0; i < array.Count(); i++)
            {
                result[i + 1] = array[i];
            }

            return result;
        }
    }
}
