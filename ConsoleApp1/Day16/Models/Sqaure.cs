using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Day16.Models
{
    internal class Sqaure
    {
        public bool IsIlluminated { get; set; }

        public bool HitFromTop { get; set; }

        public bool HitFromRight { get; set; }
        public bool HitFromBottom { get; set; }
        public bool HitFromLeft { get; set; }

        public char Content { get; set; }

        public bool IsMirror { get => this.Content == '|' || this.Content == '/' || this.Content == '-' || this.Content == '\\'; } 
    }
}
