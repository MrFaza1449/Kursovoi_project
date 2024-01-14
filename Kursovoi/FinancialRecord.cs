namespace Kursovoi
{
    public class FinancialRecord
    {
        public decimal Income { get; set; }
        public List<ExpenseItem> Expenses { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; } 
        public string IncomeComment { get; set; }
        public string IncomeSource { get; set; }

        public FinancialRecord()
        {
            Expenses = new List<ExpenseItem>();
        }
    }
    public class ExpenseItem
    {
        public decimal Amount { get; set; }
        public string Source { get; set; }
        public string? Comment { get; internal set; }
    }
}
