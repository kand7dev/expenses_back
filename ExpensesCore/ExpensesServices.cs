using ExpensesDb;

namespace ExpensesCore
{
    public class ExpensesServices: IExpensesServices
    {
        private ExpenseDbContext _context;
        public ExpensesServices(ExpenseDbContext context) => _context = context;

        public List<Expense> GetExpenses() => _context.Expenses.ToList();
    }
}
