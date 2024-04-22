
namespace ExpensesCore.DTO
{
    public class Expense
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public double Amount { get; set; }

        public static explicit operator Expense(ExpensesDb.Expense e) => new Expense
        {
            Id = e.Id,
            Amount = e.Amount,
            Description = e.Description
        };
    }
}
