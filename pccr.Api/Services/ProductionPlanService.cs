using ppcr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace powerplant_coding_challenge_remastered.Service
{
    public class ProductionPlanService : IProductionPlanService
    {

        public Payload _payload { get; set; }
        private const string PowerPlantType_GasFired = "gasfired";
        private const string PowerPlantType_Turbojet = "turbojet";
        private const string PowerPlantType_WindTurbine = "windturbine";
        private const double CreatedCo2_GasFired = 0.3;
        private const double CreatedCo2_Turbojet = 0.6;
        private const double CreatedCo2_WindTurbine = 0;

        public ProductionPlan CreateProductionPlan(Payload payload)
        {
            _payload = payload;
            CalculateCostPowerplant();
            OrderPowerplant();
            return CalculateProductionPlan();
        }


        public void AssignExternalFactorsToPowerPlant(Powerplant powerplant)
        {
            powerplant.Availabilty = 100;
            if (powerplant.Type.Equals(PowerPlantType_GasFired, StringComparison.OrdinalIgnoreCase))
            {
                powerplant.CostFuel = _payload.Fuels.Gas;
                powerplant.CostCo2 = _payload.Fuels.Co2;
                powerplant.CreatedCo2 = CreatedCo2_GasFired;
            }
            else if (powerplant.Type.Equals(PowerPlantType_Turbojet, StringComparison.OrdinalIgnoreCase))
            {
                powerplant.CostFuel = _payload.Fuels.Kerosine;
                powerplant.CostCo2 = _payload.Fuels.Co2;
                powerplant.CreatedCo2 = CreatedCo2_Turbojet;
            }
            else if (powerplant.Type.Equals(PowerPlantType_WindTurbine, StringComparison.OrdinalIgnoreCase))
            {
                powerplant.Availabilty = _payload.Fuels.Wind;
                powerplant.CostFuel = 0;
                powerplant.CostCo2 = 0;
                powerplant.CreatedCo2 = CreatedCo2_WindTurbine;
            }
        }
        public void CalculateCostPowerplant()
        {
            // Calculate cost to generate power for each powerplant

            foreach (Powerplant powerplant in _payload.Powerplants)
            {
                AssignExternalFactorsToPowerPlant(powerplant);
                powerplant.CostToGeneratePower = 1 / powerplant.Efficiency * powerplant.CostFuel;
                powerplant.CostToGeneratePower += (powerplant.CreatedCo2 * powerplant.CostCo2);
            }
            
        }

        public void OrderPowerplant()
        {
            _payload.Powerplants = _payload.Powerplants.OrderBy(x => x.CostToGeneratePower).ToList();

        }

        public ProductionPlan CalculateProductionPlan()
        {
            ProductionPlan productionPlan = new ProductionPlan(){ ProductionPlanItems = new List<ProductionPlanItem>()};

            double powerToProvide = _payload.Load;
            for (int actualPowerplantPosition = 0; actualPowerplantPosition < _payload.Powerplants.Count; actualPowerplantPosition++)
            {
                Powerplant powerplant = _payload.Powerplants[actualPowerplantPosition];
                double power = 0;
                if (powerToProvide >= powerplant.Pmin)
                {
                    power = powerToProvide >= powerplant.Pmax * (powerplant.Availabilty / 100) ?
                               powerplant.Pmax * (powerplant.Availabilty / 100) : powerToProvide;

                    double remainingPower = powerToProvide - power;
                    if (actualPowerplantPosition + 1 < _payload.Powerplants.Count) 
                    {
                        if (remainingPower > 0 && remainingPower <= _payload.Powerplants[actualPowerplantPosition + 1].Pmin)
                        {
                            if (power >= _payload.Powerplants[actualPowerplantPosition + 1].Pmin - remainingPower) 
                                power -= (_payload.Powerplants[actualPowerplantPosition + 1].Pmin - remainingPower); 
                        }
                    }

                    powerToProvide -= power;
                }
                productionPlan.ProductionPlanItems.Add(new ProductionPlanItem() { Name = powerplant.Name, P = Math.Round(power, 1) });
            }
            return productionPlan;


        }
    }
}
