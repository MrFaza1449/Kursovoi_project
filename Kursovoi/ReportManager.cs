namespace Kursovoi
{
    public class ReportManager
    {
        private List<FinancialRecord> records;

        public ReportManager(List<FinancialRecord> records)
        {
            this.records = records;
        }

        public void GenerateReport()
        {
            Console.WriteLine("Выберите период:");
            Console.WriteLine("1. За последний месяц");
            Console.WriteLine("2. За последние два месяца");
            Console.WriteLine("3. За весь период");
            Console.WriteLine("4. Ввести конкретные даты");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || (choice != 1 && choice != 2 && choice != 3 && choice != 4))
            {
                Console.WriteLine("Некорректный ввод. Выберите 1, 2, 3 или 4:");
            }

            DateTime startDate;
            DateTime endDate;

            if (choice == 1)
            {
                startDate = DateTime.Now.AddMonths(-1).Date;
                endDate = DateTime.Now.Date;
            }
            else if (choice == 2)
            {
                startDate = DateTime.Now.AddMonths(-2).Date;
                endDate = DateTime.Now.Date;
            }
            else if (choice == 3)
            {
                startDate = DateTime.MinValue;
                endDate = DateTime.MaxValue;
            }
            else
            {
                Console.WriteLine("Введите начальную дату в формате ГГГГ-ММ-ДД:");
                while (!DateTime.TryParse(Console.ReadLine(), out startDate))
                {
                    Console.WriteLine("Некорректный ввод даты. Повторите ввод:");
                }

                Console.WriteLine("Введите конечную дату в формате ГГГГ-ММ-ДД:");
                while (!DateTime.TryParse(Console.ReadLine(), out endDate))
                {
                    Console.WriteLine("Некорректный ввод даты. Повторите ввод:");
                }
            }

            var filteredRecords = records.FindAll(r => r.Date >= startDate && r.Date <= endDate);

            if (filteredRecords.Count == 0)
            {
                Console.WriteLine("Нет данных для отображения в выбранном периоде.");
                return;
            }

            Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-15} {4,-25} {5,-25} {6,-25}",
                "Дата", "Доход", "Источник дохода", "Комментарий", "Расход", "Источник расхода", "Комментарий");

            decimal totalIncome = 0;
            decimal totalExpense = 0;

            foreach (var record in filteredRecords)
            {
                Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-15} {4,-25} {5,-25} {6,-25}",
                    record.Date.ToShortDateString(), record.Income, record.IncomeSource, record.IncomeComment,
                    record.Expenses.Sum(e => e.Amount), record.Expenses.FirstOrDefault()?.Source,
                    record.Expenses.FirstOrDefault()?.Comment);

                totalIncome += record.Income;
                totalExpense += record.Expenses.Sum(e => e.Amount);

                foreach (var expense in record.Expenses)
                {
                    Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-15} {4,-25} {5,-25} {6,-25}",
                        string.Empty, string.Empty, string.Empty, string.Empty, expense.Amount, expense.Source, expense.Comment);
                }
            }

            decimal budgetRemaining = totalIncome - totalExpense;

            Console.WriteLine("{0,-15} {1,-15} {2,-25} {3,-15} {4,-25} {5,-25} {6,-25}",
                "Итог", totalIncome, string.Empty, string.Empty, totalExpense, string.Empty, string.Empty);

            Console.WriteLine($"Суммарный доход за выбранный период: {totalIncome}");
            Console.WriteLine($"Суммарный расход за выбранный период: {totalExpense}");
            Console.WriteLine($"Остаток в бюджете за выбранный период: {budgetRemaining}");
            Console.WriteLine("Нажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }
}
