using ExpensesCore;
using ExpensesCore.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesServices _expensesServices;
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
        public IActionResult CreateExpense(ExpensesDb.Expense expense)
        {
            var createdExpense = _expensesServices.CreateExpense(expense);
            return CreatedAtRoute("GetExpense", new {createdExpense.Id}, createdExpense);
        }
        [HttpDelete]
        public IActionResult DeleteExpense(Expense expense)
        {
            var result = _expensesServices.DeleteExpense(expense);
            return result ? Ok() : NotFound();
        }
        [HttpPut]
        public IActionResult EditExpense(Expense expense)
        {
            var result = _expensesServices.EditExpense(expense);
            if (result is not null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        

    }
}
