using ExpensesCore;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private IExpensesServices _expensesServices;
        public ExpensesController(IExpensesServices expensesServices) => _expensesServices = expensesServices;


        [HttpGet]
        public IActionResult GetExpenses() => Ok(_expensesServices.GetExpenses());

    }
}
