using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ppcr.Domain.Entities
{
    public class ProductionPlanItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("p")]
        public double P { get; set; }
    }
}
