using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTFAssignment.Engine.Mappings.Base;
using TTFAssignment.Engine.Model;

namespace TTFAssignment.Engine.Mappings
{
    public class SpecializedCase1Mapping : BaseCaseMapping
    {
        public override void Setup()
        {
            base.Setup();

            //X = R => Y = 2D + (D * E / 100)
            OverwriteCalculation(CalculationType.R, (d, e, f) => { return d * 2 + (d * e / 100); });
        }
    }
}
