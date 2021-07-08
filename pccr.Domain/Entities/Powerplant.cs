using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ppcr.Domain.Entities
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
