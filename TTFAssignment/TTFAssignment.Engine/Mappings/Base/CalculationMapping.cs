using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TTFAssignment.Engine.Model;

namespace TTFAssignment.Engine.Mappings.Base
{
    // Mappings copy and pasted from spec document

    //Base
    //A && B && !C => X = S
    //A && B && C => X = R
    //!A && B && C => X =T
    //[other] => [error]

    //X = S => Y = D + (D * E / 100)
    //X = R => Y = D + (D * (E - F) / 100)
    //X = T => Y = D - (D * F / 100)

    //Specialized 1
    //X = R => Y = 2D + (D * E / 100)

    //Specialized 2
    //A && B && !C => X = T
    //A && !B && C => X = S
    //X = S => Y = F + D + (D * E / 100)

    public abstract class CalculationMapping
    {

        public CalculationMapping()
        {
            CalculationResolverStrategies = new List<MappingResolverMatch>();
            CalculationStrategies = new Dictionary<CalculationType, Func<int, int, int, decimal>>();
            Setup();
        }

        public abstract void Setup();

        public async Task<(CalculationType CalculationType, decimal CalculationValue)> Resolve(bool a, bool b, bool c, int d, int e, int f)
        {
            foreach (var resolver in CalculationResolverStrategies)
            {
                if (a == resolver.A && b == resolver.B && c == resolver.C)
                {
                    var potentiallyLongCalculation = CalculationStrategies[resolver.MatchedCalculation].Invoke(d, e, f);
                    var result = await Task.FromResult<decimal>(potentiallyLongCalculation);
                    return (resolver.MatchedCalculation, result); ;
                }
            }
            return (CalculationType.Error, 0M);
        }
        
        protected List<MappingResolverMatch> CalculationResolverStrategies;
        protected Dictionary<CalculationType, Func<int, int, int, decimal>> CalculationStrategies;

        protected void OverwriteCalculationMapping(bool a, bool b, bool c, CalculationType calculationType)
        {
            var existingMatchingResolver = CalculationResolverStrategies.Where(x => x.MatchedCalculation == calculationType).SingleOrDefault();
            if (existingMatchingResolver != null)
            {
                existingMatchingResolver.A = a;
                existingMatchingResolver.B = b;
                existingMatchingResolver.C = c;
            }
            else
            {
                CalculationResolverStrategies.Add(new MappingResolverMatch { A = a, B = b, C = c, MatchedCalculation = calculationType });
            }
        }

        protected void OverwriteCalculation(CalculationType calculationType, Func<int, int, int, decimal> calculation)
        {
            if (CalculationStrategies.ContainsKey(calculationType))
            {
                CalculationStrategies[calculationType] = calculation;
            }
            else
            {
                CalculationStrategies.Add(calculationType, calculation);
            }
        }

    }
}
