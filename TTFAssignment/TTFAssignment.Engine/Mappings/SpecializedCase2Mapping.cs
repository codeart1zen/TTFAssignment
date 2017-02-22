using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTFAssignment.Engine.Mappings.Base;
using TTFAssignment.Engine.Model;

namespace TTFAssignment.Engine.Mappings
{
    public class SpecializedCase2Mapping : BaseCaseMapping
    {
        public override void Setup()
        {
            base.Setup();

            //Specialized 2
            //A && B && !C => X = T
            //A && !B && C => X = S
            //X = S => Y = F + D + (D * E / 100)

            OverwriteCalculationMapping(true, true, false, CalculationType.T);

            OverwriteCalculationMapping(true, false, true, CalculationType.S);

            OverwriteCalculation(CalculationType.S, (d, e, f) => { return f + d + (d * e / 100); });
            
        }
    }
}
