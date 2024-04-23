
using ExpensesCore.DTO;

namespace ExpensesCore
{
    public interface IExpensesServices
    {
        List<Expense> GetExpenses();
        Expense GetExpense(int id);
        Expense CreateExpense(ExpensesDb.Expense expense);
        Expense DeleteExpense(Expense expense);
        Expense EditExpense(Expense expense);
    }
}
