namespace FinancialControl.Models.Entities

{
    public class Transaction
    {
        public int Id { get; set; }

        public  string Description { get; set; }

        public  double Amount { get; set; }

        public string? Type { get; set; }

        public  DateTime Date { get; set; } = DateTime.Now;

        public Transaction(string description, double amount, string type)
        {
            var random = new Random();

            Id = random.Next(1, 1000);
            Description = description;
            Amount = amount;
            Type = type;
        }
    }
}
