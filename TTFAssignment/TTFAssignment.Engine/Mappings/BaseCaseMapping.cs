using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTFAssignment.Engine.Mappings.Base;
using TTFAssignment.Engine.Model;

namespace TTFAssignment.Engine.Mappings
{
    public class BaseCaseMapping : CalculationMapping
    {
        public override void Setup()
        {
            OverwriteCalculationMapping(true, true, false, CalculationType.S);
            OverwriteCalculationMapping(true, true, true, CalculationType.R);
            OverwriteCalculationMapping(false, true, true, CalculationType.T);

            OverwriteCalculation(CalculationType.S, (d, e, f) => { return d + (d * e / 100); });
            OverwriteCalculation(CalculationType.R, (d, e, f) => { return d + (d * (e - f) / 100); });
            OverwriteCalculation(CalculationType.T, (d, e, f) => { return d - (d * f / 100); });
        }
    }
}
