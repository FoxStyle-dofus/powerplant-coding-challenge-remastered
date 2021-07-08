using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powerplant_coding_challenge_remastered.Models
{
    public class Powerplant
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public double Efficiency { get; set; }
        public double Pmin { get; set; }
        public double Pmax { get; set; }
    }
}
