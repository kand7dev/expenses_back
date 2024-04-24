using ExpensesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsServices _statisticsServices;
        
        public StatisticsController(IStatisticsServices statisticsServices)
        {
            _statisticsServices = statisticsServices;
        }

        [HttpGet]
        public IActionResult GetExpenseAmountPerCategory()
        {
            return Ok(_statisticsServices.GetExpenseAmountPerCategory());
        }
    }
}
