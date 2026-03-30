namespace FinancialControl.Models.Entities

{
    public class Transaction
    {
        public int Id { get; set; }

        public required string Description { get; set; }

        public required double Amount { get; set; }

        public string? Type { get; set; }

        public required DateTime Date { get; set; } = DateTime.Now;
    }
}
