using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Extensions
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

        public static int[] AddArray(this int[] a, int[] b)
        {
            return a.Zip(b, (x, y) => x + y).ToArray();
        }

        public static bool ArraysEqual(this int[] a, int[] b)
        {
            if (a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
