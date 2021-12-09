using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using powerplant_coding_challenge_remastered.Service;
using ppcr.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace powerplant_coding_challenge_remastered.Controllers
{
    [Route("productionplan")]
    [ApiController]
    public class ProductionPlanController : ControllerBase
    {
        private readonly IProductionPlanService _productionPlanService;
        public ProductionPlanController(IProductionPlanService productionPlanService) => _productionPlanService = productionPlanService;

        // POST: api/productionplan
        [HttpPost]
        public async Task<ActionResult<ProductionPlan>> CalculateProductionPlan(Payload payload)
        {
            return _productionPlanService.CreateProductionPlan(payload);
        }
    }
}

