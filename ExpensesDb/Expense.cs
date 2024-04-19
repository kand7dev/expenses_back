using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesDb
{
    public class Expense
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public double Amount { get; set; }
        public required User User { get; set; }

    }
}
