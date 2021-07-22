using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ppcr.Domain.Entities
{
    public class Payload
    {
        public double Load { get; set; }
        public Fuels Fuels { get; set; }
        public List<Powerplant> Powerplants { get; set; }
    }
}
