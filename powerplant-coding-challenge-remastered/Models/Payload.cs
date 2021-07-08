using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powerplant_coding_challenge_remastered.Models
{
    public class Payload
    {
        public double Load { get; set; }
        public Fuels Fuels { get; set; }
        public List<Powerplant> PowerPlants { get; set; }
    }
}
