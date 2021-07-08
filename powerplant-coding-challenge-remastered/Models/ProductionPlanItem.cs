using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace powerplant_coding_challenge_remastered.Models
{
    public class ProductionPlanItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("p")]
        public double P { get; set; }
    }
}
