using pccr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public double CostToGeneratePower { get; set; }
        public double CostFuel { get; set; }
        public double CostCo2 { get; set; }
        public double CreatedCo2 { get; set; }
        public double Availabilty { get; set; }

        public Powerplant(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public Powerplant() { }
        public Powerplant(string name, string type, double efficiency)
        {
            Name = name;
            Type = type;
            Efficiency = efficiency;
        }

        public FuelType GetPowerplanFuelType()
        {
            switch (this.Type)
            {
                case "gasfired": return FuelType.Gas;
                case "turbojet": return FuelType.Kerosine;
                case "windturbine": return FuelType.Wind;
                default: return FuelType.Gas;
            }
        }
    }
}
