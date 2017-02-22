using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTFAssignment.Engine.Mappings;
using System.Net.Http;
using TTFAssignment.Web.Model;
using System.Net;

namespace TTFAssignment.Web.Controllers
{

    [Route("calc/[action]/{a}/{b}/{c}/{d}/{e}/{f}")]
    public class CalculationController : Controller
    {
        CalculationResultDTO ResultsDTO = new CalculationResultDTO();

        [HttpGet]
        public async Task<IActionResult> CalculateBaseCase(bool a, bool b, bool c, int d, int e, int f)
        {
            return await MapCalculationToResultsAndSend(a, b, c, d, e, f, new BaseCaseMapping());
        }

        [HttpGet]
        public async Task<IActionResult> CalculateSpecializedCase1(bool a, bool b, bool c, int d, int e, int f)
        {
            return await MapCalculationToResultsAndSend(a, b, c, d, e, f, new BaseCaseMapping());
        }

        [HttpGet]
        public async Task<IActionResult> CalculateSpecializedCase2(bool a, bool b, bool c, int d, int e, int f)
        {            
            return await MapCalculationToResultsAndSend(a, b, c, d, e, f, new BaseCaseMapping());
        }

        async Task<IActionResult> MapCalculationToResultsAndSend(bool a, bool b, bool c, int d, int e, int f, BaseCaseMapping mapper)
        {
            (ResultsDTO.X, ResultsDTO.Y) = await mapper.Resolve(a, b, c, d, e, f);

            if (ResultsDTO.X == Engine.Model.CalculationType.Error) throw new HttpRequestException("Invalid request parameters");

            return new ObjectResult(ResultsDTO);
        }
    }
}
