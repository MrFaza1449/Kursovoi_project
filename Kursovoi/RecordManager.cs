namespace Kursovoi
{
    public class RecordManager
    {
        private List<FinancialRecord> records;

        public RecordManager(List<FinancialRecord> records)
        {
            this.records = records;
        }
        public string? answer { get; private set; }

        public void AddRecord()
        {
            Console.Write("Что вы хотите добавить: (1) Доходы или (2) Расходы? ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2))
            {
                Console.WriteLine("Некорректный ввод. Введите 1 для добавления доходов или 2 для добавления расходов:");
            }

            if (choice == 1)
            {
                Console.Write("Введите сумму дохода: ");
                decimal income;
                while (!decimal.TryParse(Console.ReadLine(), out income))
                {
                    Console.WriteLine("Некорректный ввод. Введите сумму дохода еще раз:");
                }

                Console.Write("Хотите указать источник дохода? (y/n): ");
                string answer = Console.ReadLine();
                string incomeSource = string.Empty;

                if (answer.ToLower() == "y")
                {
                    Console.Write("Введите источник дохода: ");
                    incomeSource = Console.ReadLine();
                }

                Console.Write("Хотите ввести комментарий к доходу? (y/n): ");
                answer = Console.ReadLine();
                string incomeComment = string.Empty;

                if (answer.ToLower() == "y")
                {
                    Console.Write("Введите комментарий к доходу: ");
                    incomeComment = Console.ReadLine();
                }
                FinancialRecord record = new FinancialRecord();
                record.Income = income;
                record.IncomeSource = incomeSource;
                record.IncomeComment = incomeComment;
                record.Date = DateTime.Now;

                records.Add(record);
            }
            else
            {
                Console.Write("Введите количество позиций расходов: ");
                int expenseCount;
                while (!int.TryParse(Console.ReadLine(), out expenseCount) || expenseCount < 0)
                {
                    Console.WriteLine("Некорректный ввод. Введите количество позиций расходов еще раз:");
                }

                List<ExpenseItem> expenses = new List<ExpenseItem>();

                for (int i = 0; i < expenseCount; i++)
                {
                    Console.Write($"Введите сумму расхода {i + 1}: ");
                    decimal expenseAmount;
                    while (!decimal.TryParse(Console.ReadLine(), out expenseAmount))
                    {
                        Console.WriteLine($"Некорректный ввод. Введите сумму расхода {i + 1} еще раз:");
                    }

                    Console.Write($"Хотите указать источник расхода {i + 1}? (y/n): ");
                    answer = Console.ReadLine();
                    string expenseSource = string.Empty;

                    if (answer.ToLower() == "y")
                    {
                        Console.Write($"Введите источник расхода {i + 1}: ");
                        expenseSource = Console.ReadLine();
                    }

                    Console.Write($"Хотите ввести комментарий к расходу {i + 1}? (y/n): ");
                    answer = Console.ReadLine();
                    string expenseComment = string.Empty;

                    if (answer.ToLower() == "y")
                    {
                        Console.Write($"Введите комментарий к расходу {i + 1}: ");
                        expenseComment = Console.ReadLine();
                    }
                    expenses.Add(new ExpenseItem { Amount = expenseAmount, Source = expenseSource, Comment = expenseComment });
                }
                FinancialRecord record = new FinancialRecord();
                record.Expenses = expenses;
                record.Date = DateTime.Now;

                records.Add(record);
            }
        }
    }
}
