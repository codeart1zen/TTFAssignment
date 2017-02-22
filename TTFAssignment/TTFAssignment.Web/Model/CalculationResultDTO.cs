using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTFAssignment.Engine.Model;

namespace TTFAssignment.Web.Model
{
    public class CalculationResultDTO
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public CalculationType X { get; set; }
        public decimal Y { get; set; }
    }
}
