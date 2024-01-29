using Microsoft.AspNetCore.Mvc;
using PersonalOnlineCalculator.Models;
using PersonalOnlineCalculator.Services;

namespace PersonalOnlineCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly CalculationService _calculationService;

        public CalculatorController(CalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] string expression)
        {
            var calculation = _calculationService.PerformCalculation(expression);
            return Ok(calculation);
        }

        [HttpGet("history/{userId}")]
        public IActionResult GetHistory(int userId)
        {
            var calculations = _calculationService.GetCalculationsForUser(userId);
            return Ok(calculations);
        }

        [HttpPost("save")]
        public IActionResult SaveCalculation([FromBody] Calculation calculation)
        {
            _calculationService.SaveCalculation(calculation.Id, calculation);
            return Ok();
        }

        [HttpDelete("delete/{calculationId}")]
        public IActionResult DeleteCalculation(int calculationId)
        {
            _calculationService.DeleteCalculation(calculationId);
            return Ok();
        }
    }
}
