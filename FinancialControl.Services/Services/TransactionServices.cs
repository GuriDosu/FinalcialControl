using FinancialControl.Services.Services.Interfaces;
using FinancialControl.Models.Entities;

namespace FinancialControl.Services.Services
{
    public class TransactionServices : ITransactionService
    {
        private readonly List<Transaction> _transactions = new();

        public Transaction Add(Transaction transaction)
        {
            _transactions.Add(transaction);
            return transaction;
        }
        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }
    }
}
