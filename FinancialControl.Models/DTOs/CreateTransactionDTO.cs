

namespace FinancialControl.Models.DTOs
{
    public class CreateTransactionDTO
    {
        public required string Description { get; set; }
        public required double Amount {  get; set; }
        public required string Type { get; set; }

    }
}
