using FinancialControl.Models.Entities;


namespace FinancialControl.Services.Services.Interfaces
{
    public interface ITransactionService
    {
        Transaction Add(Transaction transaction);
        List<Transaction> GetTransactions();
    }
}
