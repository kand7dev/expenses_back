using ExpensesDb;
using Microsoft.Identity.Client;

namespace ExpensesCore
{
    public class ExpensesServices : IExpensesServices
    {
        private ExpenseDbContext _context;
        public ExpensesServices(ExpenseDbContext context) => _context = context;
        public Expense? GetExpense(int id)
        {
            Expense? expenseById = null;

            try
            {
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

        public bool DeleteExpense(Expense expense)
        {
            _context.Remove(expense);
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Expense? EditExpense(Expense expense)
        {
            try
            {
                var dbExpense = _context.Expenses.First(e => e.Id == expense.Id);
                dbExpense.Description = expense.Description;
                dbExpense.Amount = expense.Amount;
                _context.SaveChanges();
                return dbExpense;
            }
            catch
            {
                return null;
            }
        }
    }
}
