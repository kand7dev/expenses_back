using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesDb
{
    public class Expense
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public double? Amount { get; set; }
        [ForeignKey("FK_UserId")]
        public required User User { get; set; }

    }
}
