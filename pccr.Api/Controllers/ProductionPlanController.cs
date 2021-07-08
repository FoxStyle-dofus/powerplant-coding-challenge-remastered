using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using powerplant_coding_challenge_remastered.Service;
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
        private readonly IProductionPlanService _productionPlanSerice;
        public ProductionPlanController(IProductionPlanService productionPlanService) => _productionPlanSerice = productionPlanService;

        // POST: api/productionplan
        [HttpPost]
        public async Task<ActionResult<object>> CalculateProductionPlan()
        {
            return Ok();
        }
    }
}

