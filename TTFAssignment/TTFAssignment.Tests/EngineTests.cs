using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TTFAssignment.Engine.Mappings;

namespace TTFAssignment.Tests
{
    [TestClass]
    public class EngineTests
    {
        const int D = 2;
        const int E = 1;
        const int F = 1;
        //const int D = 5;
        //const int E = 7;
        //const int F = 9;

        readonly decimal BaseCaseCalculationTypeSCorrectAnswer = D + (D * E / 100); // X = S => Y = D + (D * E / 100)
        readonly decimal BaseCaseCalculationTypeRCorrectAnswer = D + (D * (E - F) / 100); // X = R => Y = D + (D * (E - F) / 100)
        readonly decimal BaseCaseCalculationTypeTCorrectAnswer = D - (D * F / 100); // X = T => Y = D - (D * F / 100)

        readonly decimal Spec1CaseCalculationTypeRCorrectAnswer = (2 * D) + (D * E / 100); // X = R => Y = 2D + (D * E / 100)

        readonly decimal Spec2CaseCalculationTypeSCorrectAnswer = F + D + (D * E / 100); // X = S => Y = D + (D * E / 100)


        [TestMethod]
        public void TestBaseCaseMapping()
        {
            var mapper = new BaseCaseMapping();
            //A && B && !C => X = S
            //A && B && C => X = R
            //!A && B && C => X =T
            //[other] => [error]
                        
            VerifyCalculation(mapper, true, true, false, Engine.Model.CalculationType.S, BaseCaseCalculationTypeSCorrectAnswer);

            VerifyCalculation(mapper, true, true, true, Engine.Model.CalculationType.R, BaseCaseCalculationTypeRCorrectAnswer);

            VerifyCalculation(mapper, false, true, true, Engine.Model.CalculationType.T, BaseCaseCalculationTypeTCorrectAnswer);

            VerifyInvalidInput(mapper, true, false, true);
            VerifyInvalidInput(mapper, false, false, true);
            VerifyInvalidInput(mapper, false, true, false);
            VerifyInvalidInput(mapper, true, false, false);
            VerifyInvalidInput(mapper, false, false, false);
        }

        [TestMethod]
        public void TestSpecializedCase1Mapping()
        {
            var mapper = new SpecializedCase1Mapping();
            //No change of mappings, only calculation
            //X = R => Y = 2D + (D * E / 100)

            VerifyCalculation(mapper, true, true, true, Engine.Model.CalculationType.R, Spec1CaseCalculationTypeRCorrectAnswer);

            VerifyCalculation(mapper, true, true, false, Engine.Model.CalculationType.S, BaseCaseCalculationTypeSCorrectAnswer);
            
            VerifyCalculation(mapper, false, true, true, Engine.Model.CalculationType.T, BaseCaseCalculationTypeTCorrectAnswer);

            VerifyInvalidInput(mapper, true, false, true);
            VerifyInvalidInput(mapper, false, false, true);
            VerifyInvalidInput(mapper, false, true, false);
            VerifyInvalidInput(mapper, true, false, false);
            VerifyInvalidInput(mapper, false, false, false);
        }

        [TestMethod]
        public void TestSpecializedCase2Mapping()
        {
            var mapper = new SpecializedCase2Mapping();
            //A && B && !C => X = T
            //A && !B && C => X = S
            //X = S => Y = F + D + (D * E / 100)

            VerifyCalculation(mapper, true, true, false, Engine.Model.CalculationType.T, BaseCaseCalculationTypeTCorrectAnswer);

            VerifyCalculation(mapper, true, false, true, Engine.Model.CalculationType.S, Spec2CaseCalculationTypeSCorrectAnswer);

            VerifyCalculation(mapper, true, true, true, Engine.Model.CalculationType.R, BaseCaseCalculationTypeRCorrectAnswer);

            VerifyInvalidInput(mapper, false, true, true);
            VerifyInvalidInput(mapper, false, false, true);
            VerifyInvalidInput(mapper, false, true, false);
            VerifyInvalidInput(mapper, true, false, false);
            VerifyInvalidInput(mapper, false, false, false);
        }

        private void VerifyInvalidInput(BaseCaseMapping mapper, bool a, bool b, bool c)
        {
            var result = mapper.Resolve(a, b, c, D, E, F).Result;
            Assert.AreEqual(result.CalculationType, Engine.Model.CalculationType.Error, "Invalid input returned an error"); // null is much faster at runtime than throwing an exception, so the calculation engine can be used in performance critical code?
        }

        private void VerifyCalculation(BaseCaseMapping mapper, bool a, bool b, bool c, Engine.Model.CalculationType calc, decimal correctValue)
        {
            var result = mapper.Resolve(a, b, c, D, E, F).Result;
            Assert.AreNotEqual(result, Engine.Model.CalculationType.Error, "Calculation Type {calc} not mapped");
            Assert.AreEqual(result.CalculationType, calc, "Calculation Type {calc} is mapped incorrectly");
            Assert.AreEqual(result.CalculationValue, correctValue, "Calculation Type {calc} has invalid calculation result");
        }
    }
}
