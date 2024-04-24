
using Microsoft.AspNetCore.Http;
namespace ExpensesCore
{
    public class StatisticsServices : IStatisticsServices
    {
        private readonly ExpensesDb.ExpenseDbContext _context;
        private readonly ExpensesDb.User _user;

        public StatisticsServices(ExpensesDb.ExpenseDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _user = _context.Users.First(u => u.Username == httpContextAccessor.HttpContext.User.Identity!.Name);
        }
        public IEnumerable<KeyValuePair<string, double?>> GetExpenseAmountPerCategory()
        {
            return _context.Expenses.Where(e => e.User!.Id == _user.Id).AsEnumerable().GroupBy(e => e.Description!).ToDictionary(e => e.Key, e => e.Sum(x => x.Amount)).Select(x => new KeyValuePair<string, double?>(x.Key, x.Value));
        }
    }
}
