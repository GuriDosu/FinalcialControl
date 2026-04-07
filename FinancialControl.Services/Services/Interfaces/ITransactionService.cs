using FinancialControl.Models.DTOs;
using FinancialControl.Models.Entities;


namespace FinancialControl.Services.Services.Interfaces
{
    public interface ITransactionService
    {
        Transaction Add(CreateTransactionDTO transaction);
        List<Transaction> GetTransactions();

        Transaction GetById(int id);
    }
}
