using ppcr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powerplant_coding_challenge_remastered.Service
{
    public interface IProductionPlanService
    {
        public ProductionPlan CreateProductionPlan(Payload payload);
        public ProductionPlan CalculateProductionPlan(); 
        public void OrderPowerplant();
        public void CalculateCostPowerplant();
        public void AssignExternalFactorsToPowerPlant(Powerplant powerplant);

    }
}
