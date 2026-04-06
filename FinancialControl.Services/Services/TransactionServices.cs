using FinancialControl.Models.DTOs;
using FinancialControl.Models.Entities;
using FinancialControl.Services.Services.Interfaces;

namespace FinancialControl.Services.Services
{
    public class TransactionServices : ITransactionService
    {
        private readonly List<Transaction> _transactions = new();

        public Transaction Add(CreateTransactionDTO transactionDTO)
        {
            if (transactionDTO.Amount <= 0)
            {
                throw new ArgumentException("O valor deve ser maior que zero.");
            }
            Transaction newTransaction = new(transactionDTO.Description, transactionDTO.Amount, transactionDTO.Type);
            _transactions.Add(newTransaction);
            return newTransaction;
        }
        public List<Transaction> GetTransactions()
        {
            return _transactions;
        }
    }
}
