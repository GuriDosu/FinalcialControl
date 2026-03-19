using FinancialControl.Services.Services.Interfaces;
using System.Transactions;


namespace FinancialControl.Services.Services
{
    public class TransactionServices : ITransactionService
    {
        private readonly List<Transaction> _transactions = new ();
        public Transaction Add(Transaction transaction)
        {
            _transactions.Add(transaction);
            return transaction;
        }
    }
}