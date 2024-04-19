
using ExpensesCore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesCore
{
    public interface IExpensesServices
    {
        List<Expense> GetExpenses();
        Expense? GetExpense(int id);
        Expense CreateExpense(ExpensesDb.Expense expense);
        bool DeleteExpense(Expense expense);
        Expense? EditExpense(Expense expense);
    }
}
