using ExpensesCore;
using ExpensesDb;
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
        [HttpGet("{id}", Name = "GetExpense")]
        public IActionResult GetExpense(int id)
        {
            var result = _expensesServices.GetExpense(id);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult CreateExpense(Expense expense)
        {
            var createdExpense = _expensesServices.CreateExpense(expense);
            return CreatedAtRoute("GetExpense", new {createdExpense.Id}, createdExpense);
        }
       

        

    }
}
