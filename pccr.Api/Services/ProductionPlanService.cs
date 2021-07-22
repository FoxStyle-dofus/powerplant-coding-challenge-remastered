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
        private const string CST_PowerPlantType_GasFired = "gasfired";
        private const string CST_PowerPlantType_Turbojet = "turbojet";
        private const string CST_PowerPlantType_WindTurbine = "windturbine";
        private const double CST_CreatedCo2_GasFired = 0.3;
        private const double CST_CreatedCo2_Turbojet = 0.6;
        private const double CST_CreatedCo2_WindTurbine = 0;

        public ProductionPlan CreateProductionPlan()
        {
            CalculateCostPowerplant();
            OrderPowerplant();
            CalculateProductionPlan();
        }


        private void AssignExternalFactorsToPowerPlant(Powerplant powerplant)
        {
            powerplant.Availabilty = 100;
            if (powerplant.Type.Equals(CST_PowerPlantType_GasFired, StringComparison.OrdinalIgnoreCase))
            {
                powerplant.CostFuel = _payload.Fuels.Gas;
                powerplant.CostCo2 = _payload.Fuels.Co2;
                powerplant.CreatedCo2 = CST_CreatedCo2_GasFired;
            }
            else if (powerplant.Type.Equals(CST_PowerPlantType_Turbojet, StringComparison.OrdinalIgnoreCase))
            {
                powerplant.CostFuel = _payload.Fuels.Kerosine;
                powerplant.CostCo2 = _payload.Fuels.Co2;
                powerplant.CreatedCo2 = CST_CreatedCo2_Turbojet;
            }
            else if (powerplant.Type.Equals(CST_PowerPlantType_WindTurbine, StringComparison.OrdinalIgnoreCase))
            {
                powerplant.Availabilty = _payload.Fuels.Wind;
                powerplant.CostFuel = 0;
                powerplant.CostCo2 = 0;
                powerplant.CreatedCo2 = CST_CreatedCo2_WindTurbine;
            }
        }
        public ProductionPlan CalculateCostPowerplant()
        {
            // Calculate cost to generate power for each powerplant

            foreach (Powerplant powerplant in _payload.Powerplants)
            {
                AssignExternalFactorsToPowerPlant(powerplant);
                powerplant.CostToGeneratePower = 1 / powerplant.Efficiency * powerplant.CostFuel;
                powerplant.CostToGeneratePower += (powerplant.CreatedCo2 * powerplant.CostCo2);
            }
            
        }

        public ProductionPlan OrderPowerplant()
        {
            _payload.Powerplants = _payload.Powerplants.OrderBy(x => x.CostToGeneratePower).ToList();

        }

        public ProductionPlan CalculateProductionPlan()
        {
            ProductionPlan productionPlan = new ProductionPlan(){ ProductionPlanItems = new List<ProductionPlanItem>()};

            double powerToProvide = _payload.Load;
            for (int i = 0; i < _payload.Powerplants.Count; i++)
            {
                Powerplant powerplant = _payload.Powerplants[i];
                double power = 0;
                if (powerToProvide >= powerplant.Pmin)
                {
                    power = powerToProvide >= powerplant.Pmax * (powerplant.Availabilty / 100) ?
                               powerplant.Pmax * (powerplant.Availabilty / 100) : powerToProvide;

                    double restPower = powerToProvide - power;
                    if (i + 1 < _payload.Powerplants.Count) 
                    {
                        if (restPower > 0 && restPower <= _payload.Powerplants[i + 1].Pmin)
                        {
                            if (power >= _payload.Powerplants[i + 1].Pmin - restPower) 
                                power -= (_payload.Powerplants[i + 1].Pmin - restPower); 
                        }
                    }

                    powerToProvide -= power;
                }
                productionPlan.ProductionPlanItems.Add(new ProductionPlanItem() { Name = powerplant.Name, P = Math.Round(power, 1) });
            }


        }
    }
}
