namespace ExpensesCore
{
    public interface IStatisticsServices
    {
        IEnumerable<KeyValuePair<string, double>> GetExpenseAmountPerCategory();
    }
}
