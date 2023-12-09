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

        public string[] ReadAllFile(string filePath)
        {
            string[] result = new string[] { "" };

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    result = File.ReadAllLines(filePath);
                }
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }



            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="process"></param>
        public void ProcessEachLine(string filePath, Action<string> process)
        {
            if (File.Exists(filePath))
            {
                // Open the file with a StreamReader
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        process(line);
                    }
                }

                Console.WriteLine("File processing completed.");
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }
        }

        public abstract string Solve();

        public abstract string SolvePart2();
    }
}
