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
            if (!_transactions.Any())
            {
                throw new InvalidOperationException("Nenhuma transação registrada até o momento");
            }
            return _transactions;
        }
        public Transaction GetTransactionById(int id)
        {
            Transaction transaction = _transactions.FirstOrDefault(x => x.Id == id);
            if (transaction == null)

            {
                throw new InvalidOperationException("Nenhuma transação encontrada com o ID fornecido.");
            }
            return transaction;
        }
        public Transaction GetById(int id)
        {
            Transaction transaction = _transactions.FirstOrDefault(x => x.Id == id);
            if (transaction == null)
            {
                throw new InvalidOperationException("Nenhuma transação encontrada com o ID fornecido.");
            }
            return transaction;
        }
        public List<Transaction> GetTransactionByType(string type)
        {
            var transactions = _transactions.Where(x => x.Type.Equals(type)).ToList();
            
            if (!transactions.Any())
            {
                throw new InvalidOperationException($"Nenhuma transação encontrada com o tipo {type}, nao foi encontrado.");
            }
          return transactions;
        }
                
    }
}

