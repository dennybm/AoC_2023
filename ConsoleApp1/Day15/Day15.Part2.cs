using ConsoleApp1.Day15;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day15
{
    internal partial class Day15
    {
        Dictionary<int, OrderedDictionary> Boxes = new();

        public override string SolvePart2()
        {
            this.ProcessEachLine(filePath, SortLenses);

            return this.CalculateFocusingPower(this.Boxes).ToString();
        }

        private int CalculateFocusingPower(Dictionary<int, OrderedDictionary> boxes)
        {
            int focusingPower = 0;

            foreach (KeyValuePair<int, OrderedDictionary> box in boxes)
            {
                int lensePosition = 1;

                foreach (object lenseObj in box.Value)
                {
                    Lense lense = lenseObj.GetType().GetProperty("Value").GetValue(lenseObj) as Lense;

                    focusingPower += (box.Key + 1) * lensePosition * lense.FocalLength;
                    lensePosition++;
                }
            }

            return focusingPower;
        }

        private void SortLenses(string input)
        {
            string[] initSequence = input.Split(',');

            foreach (string init in initSequence)
            {
                ProcessInit(init);
            }
        }

        private void ProcessInit(string init)
        {
            string label = string.Empty;
            int focalLength = 0;

            if (init.Last() == '-')
            {
                label = init.Substring(0,init.Length - 1);
                int hashValue = GetHash(label);
                if (Boxes.ContainsKey(hashValue))
                {
                    Boxes[hashValue].Remove(label);
                }
            }
            else
            {
                label = init.Split('=')[0];
                focalLength = int.Parse(init.Split('=')[1]);

                Lense lense = new Lense() { FocalLength = focalLength, Label = label };

                if (Boxes.ContainsKey(lense.HashValue))
                {
                    Boxes[lense.HashValue][label] = lense;
                }
                else
                {
                    Boxes[lense.HashValue] = new OrderedDictionary{ { label, lense } };
                }
            }
        }
    }
}

public class Lense
{
    public int FocalLength { get; set; }

    public string Label { get; set; }   

    public int HashValue { get => Day15.GetHash(Label); }
}
