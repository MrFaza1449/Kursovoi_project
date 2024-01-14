using Newtonsoft.Json;


namespace Kursovoi
{
    public class FinanceManager
    {
        private List<FinancialRecord> records;

        public FinanceManager()
        {
            LoadData();
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить запись");
                Console.WriteLine("2. Сформировать отчет");
                Console.WriteLine("3. Выйти");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddRecord();
                        break;
                    case "2":
                        GenerateReport();
                        break;
                    case "3":
                        SaveData();
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Повторите попытку.");
                        break;
                }
            }
        }

        private void LoadData()
        {
            try
            {
                string json = File.ReadAllText("Report.json");
                records = JsonConvert.DeserializeObject<List<FinancialRecord>>(json);
            }
            catch (FileNotFoundException)
            {
                records = new List<FinancialRecord>();
            }
        }

        private void SaveData()
        {
            string json = JsonConvert.SerializeObject(records, Formatting.Indented);
            File.WriteAllText("Report.json", json);
        }

        private void AddRecord()
        {
            RecordManager recordManager = new RecordManager(records);
            recordManager.AddRecord();
            SaveData();
            Console.WriteLine("Запись добавлена.");
        }

        private void GenerateReport()
        {
            ReportManager reportManager = new ReportManager(records);
            reportManager.GenerateReport();
        }
    }
}
