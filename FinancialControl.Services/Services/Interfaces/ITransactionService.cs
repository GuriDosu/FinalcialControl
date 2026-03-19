using System.Transactions;


namespace FinancialControl.Services.Services.Interfaces
{
    public interface ITransactionService
    {
        Transaction Add(Transaction transaction);
    }
}
