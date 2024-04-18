using ExpensesDb;
using Microsoft.Identity.Client;

namespace ExpensesCore
{
    public class ExpensesServices: IExpensesServices
    {
        private ExpenseDbContext _context;
        public ExpensesServices(ExpenseDbContext context) => _context = context;
        public Expense? GetExpense(int id) {
            Expense? expenseById = null;

            try {
                expenseById = _context.Expenses.First(e => e.Id == id);
                return expenseById;
            }
            catch
            {
                
                return null;
            }

        }          
        public List<Expense> GetExpenses() => _context.Expenses.ToList();
        public Expense CreateExpense(Expense expense)
        {
            _context.Add(expense);
            _context.SaveChanges();
            return expense;
        }

    }
}
