using ExpensesCore.CustomExceptions;
using ExpensesCore.DTO;
using Microsoft.AspNetCore.Http;

namespace ExpensesCore
{
    public class ExpensesServices : IExpensesServices
    {
        private readonly ExpensesDb.ExpenseDbContext _context;
        private readonly ExpensesDb.User _user;
        public ExpensesServices(ExpensesDb.ExpenseDbContext context, IHttpContextAccessor httpContextAccessor )
        {
            _context = context;
            _user = _context.Users.First(u => u.Username == httpContextAccessor.HttpContext.User.Identity!.Name);
        }
        public Expense GetExpense(int id)
        {
            try
            {
                var expense = _context.Expenses.Where(e => e.User!.Id == _user.Id && e.Id == id).Select(e => (Expense)e).First();
                return expense;
            }
            catch
            {
                throw new NotFoundException();
            }
        }
        public List<Expense> GetExpenses() => [.. _context.Expenses.Where(e => e.User!.Id == _user.Id).Select(e => (Expense)e)];
        public ExpensesCore.DTO.Expense CreateExpense(ExpensesDb.Expense expense)
        {
            expense.User = _user;
            _context.Add(expense);
            _context.SaveChanges();
            return (Expense)expense;
        }

        public Expense DeleteExpense(Expense expense)
        {
            var dbExpense = _context.Expenses.First(e => e.User!.Id == _user.Id && e.Id == expense.Id);
            _context.Remove(dbExpense);
            try
            {
                _context.SaveChanges();
                return (Expense)dbExpense;
            }
            catch
            {
                throw new NotFoundException();
            }
        }

        public Expense EditExpense(Expense expense)
        {
            try
            {
                var dbExpense = _context.Expenses.First(e => e.User!.Id == _user.Id && e.Id == expense.Id);
                dbExpense.Description = expense.Description;
                dbExpense.Amount = expense.Amount;
                _context.SaveChanges();
                return expense;
            }
            catch (NotFoundException)
            {
                throw new NotFoundException();
            }
        }
    }
}
