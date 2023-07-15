namespace Budget_App.Models
{
    public class ExpenseDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ExpenseCollectionName { get; set; } = null!;
    }
}
