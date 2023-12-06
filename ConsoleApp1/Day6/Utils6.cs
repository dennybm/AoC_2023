using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day6
{
    internal static class Utils6
    {
        /// <summary>
        /// Gets the roots of the equation if they are real.
        /// </summary>
        /// <param name="a">Square coeficient</param>
        /// <param name="b">Linear coeficient</param>
        /// <param name="c">Constant coeficient</param>
        /// <returns></returns>
        public static double[]? SolvePolynomial(long a, long b, long c)
        {
            double[]? result = new double[2];

            double descriminator = b * b - 4 * a * c;

            if (descriminator < 0)
            {
                return null;
            }
            else
            {
                result[0] = (-b - Math.Sqrt(descriminator)) / (2 * a);
                result[1] = (-b + Math.Sqrt(descriminator)) / (2 * a);
            }

            return result;
        }
    }
}
