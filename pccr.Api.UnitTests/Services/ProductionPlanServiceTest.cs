using powerplant_coding_challenge_remastered.Service;
using ppcr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace pccr.Api.UnitTests.Services
{
    public class ProductionPlanServiceTest : IClassFixture<ProductionPlanService>
    {
        ProductionPlanService _productionPlanService = new ProductionPlanService();
        private const string PowerPlantType_GasFired = "gasfired";
        private const string PowerPlantType_Turbojet = "turbojet";
        private const string PowerPlantType_WindTurbine = "windturbine";

        [Fact]
        public void AssignExternalFactorsToPowerPlantTest()
        {
            _productionPlanService._payload = new Payload { Fuels = new Fuels { Gas = 1, Kerosine = 3, Wind = 4 } };
            Powerplant gasPowerplant = new Powerplant("gasPowerplan", PowerPlantType_GasFired);
            Powerplant turbojetPowerplant = new Powerplant("turbojetPowerplant", PowerPlantType_Turbojet);
            Powerplant windTurbinePowerplant = new Powerplant("windTurbinePowerplant", PowerPlantType_WindTurbine);
            _productionPlanService.AssignExternalFactorsToPowerPlant(gasPowerplant);
            _productionPlanService.AssignExternalFactorsToPowerPlant(turbojetPowerplant);
            _productionPlanService.AssignExternalFactorsToPowerPlant(windTurbinePowerplant);
            Assert.Equal(gasPowerplant.CostFuel, _productionPlanService._payload.Fuels.Gas);
            Assert.Equal(turbojetPowerplant.CostFuel, _productionPlanService._payload.Fuels.Kerosine);
            Assert.Equal(windTurbinePowerplant.Availabilty, _productionPlanService._payload.Fuels.Wind);
        }

        [Fact]
        public void CalculateCostPowerPlantTest()
        {
            List<Powerplant> powerplants = new List<Powerplant>();
            powerplants.Add(new Powerplant("gasPowerplan", PowerPlantType_GasFired, 100));
            _productionPlanService._payload = new Payload { Powerplants = powerplants, Fuels = new Fuels { Gas = 1, Kerosine = 3, Wind = 4 } };
            _productionPlanService.CalculateCostPowerplant();
            Assert.Equal(0.01, _productionPlanService._payload.Powerplants[0].CostToGeneratePower);
        }

        [Fact]
        public void OrderPowerplantTest()
        {
            List<Powerplant> powerplants = new List<Powerplant>();
            powerplants.Add(new Powerplant("gasPowerplan", PowerPlantType_GasFired, 100));
            powerplants.Add(new Powerplant("turbojetPowerplant", PowerPlantType_Turbojet, 100));
            powerplants.Add(new Powerplant("windTurbinePowerplant", PowerPlantType_WindTurbine, 100));
            _productionPlanService._payload = new Payload { Powerplants = powerplants, Fuels = new Fuels { Gas = 1, Kerosine = 3, Wind = 4 } };
            _productionPlanService.CalculateCostPowerplant();
            _productionPlanService.OrderPowerplant();
            Assert.True(_productionPlanService._payload.Powerplants[0].Type == PowerPlantType_WindTurbine
                     && _productionPlanService._payload.Powerplants[1].Type == PowerPlantType_GasFired
                     && _productionPlanService._payload.Powerplants[2].Type == PowerPlantType_Turbojet);
        }

        [Fact]

        public void CreateProductionPlanTest()
        {
            Payload payload = new Payload();
            payload.Load = 480;
            payload.Fuels = new Fuels() { Gas = 13.4, Kerosine = 50.8, Co2 = 20, Wind = 60 };
            payload.Powerplants = new List<Powerplant>();
            payload.Powerplants.Add(new Powerplant() { Name = "gasfiredbig1", Type = "gasfired", Efficiency = 0.53 , Pmin = 100 , Pmax = 460 });
            payload.Powerplants.Add(new Powerplant() { Name = "gasfiredbig2", Type = "gasfired", Efficiency = 0.53, Pmin = 100, Pmax = 460 });
            payload.Powerplants.Add(new Powerplant() { Name = "gasfiredsomewhatsmaller", Type = "gasfired", Efficiency = 0.37, Pmin = 40, Pmax = 210 });
            payload.Powerplants.Add(new Powerplant() { Name = "tj1", Type = "turbojet", Efficiency = 0.3, Pmin = 0, Pmax = 16 });
            payload.Powerplants.Add(new Powerplant() { Name = "windpark1", Type = "windturbine", Efficiency = 1, Pmin = 0, Pmax = 150 });
            payload.Powerplants.Add(new Powerplant() { Name = "windpark2", Type = "windturbine", Efficiency = 1, Pmin = 0, Pmax = 36 });
            ProductionPlan result = _productionPlanService.CreateProductionPlan(payload);
            List<ProductionPlanItem> expectedProductionPlanItemsResult = new List<ProductionPlanItem>();
            expectedProductionPlanItemsResult.Add(new ProductionPlanItem() { Name = "windpark1", P = 90});
            expectedProductionPlanItemsResult.Add(new ProductionPlanItem() { Name = "windpark2", P = 21.6 });
            expectedProductionPlanItemsResult.Add(new ProductionPlanItem() { Name = "gasfiredbig1", P = 368.4 });
            expectedProductionPlanItemsResult.Add(new ProductionPlanItem() { Name = "gasfiredbig2", P = 0 });
            expectedProductionPlanItemsResult.Add(new ProductionPlanItem() { Name = "gasfiredsomewhatsmaller", P = 0 });
            expectedProductionPlanItemsResult.Add(new ProductionPlanItem() { Name = "tj1", P = 0 });
            Assert.True(expectedProductionPlanItemsResult[0].Name == result.ProductionPlanItems[0].Name 
                && expectedProductionPlanItemsResult[0].P == result.ProductionPlanItems[0].P);
            Assert.True(expectedProductionPlanItemsResult[1].Name == result.ProductionPlanItems[1].Name 
                && expectedProductionPlanItemsResult[1].P == result.ProductionPlanItems[1].P);
            Assert.True(expectedProductionPlanItemsResult[2].Name == result.ProductionPlanItems[2].Name 
                && expectedProductionPlanItemsResult[2].P == result.ProductionPlanItems[2].P);
            Assert.True(expectedProductionPlanItemsResult[3].Name == result.ProductionPlanItems[3].Name 
                && expectedProductionPlanItemsResult[3].P == result.ProductionPlanItems[3].P);
            Assert.True(expectedProductionPlanItemsResult[4].Name == result.ProductionPlanItems[4].Name 
                && expectedProductionPlanItemsResult[4].P == result.ProductionPlanItems[4].P);
            Assert.True(expectedProductionPlanItemsResult[5].Name == result.ProductionPlanItems[5].Name 
                && expectedProductionPlanItemsResult[5].P == result.ProductionPlanItems[5].P);

        }
    }
}
