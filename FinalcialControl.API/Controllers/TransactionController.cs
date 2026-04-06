using FinancialControl.Models.DTOs;
using FinancialControl.Models.Entities;
using FinancialControl.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace FinancialControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpGet]
        public IActionResult Gettransactions()
        {
            var transactions = _transactionService.GetTransactions();
            return Ok(transactions);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateTransactionDTO transaction)
        {
            try
            {
                var transactionCreated = _transactionService.Add(transaction);
                return Ok(transactionCreated);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
