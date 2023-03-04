using Campus02DemoProject.Models;
using Campus02DemoProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Campus02DemoProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationServiceController : ControllerBase
    {
        private ICalcService calcService;
        public CalculationServiceController(ICalcService _calcService) {
        calcService = _calcService;
        }

        [HttpPost]
        public IActionResult PostCalculation(CalcModel calcModel)
        {
            //CalcService _calcService = new CalcService();
            return Ok(calcService.Sum(calcModel));
        }
    }
}
