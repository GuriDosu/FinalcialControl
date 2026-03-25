using FinancialControl.Services.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using FinancialControl.Models.Entities;



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

        [HttpPost]
        public IActionResult Create([FromBody]Transaction transaction)
        {
            var transactionCreated = _transactionService.Add(transaction);
            return Ok(transactionCreated);
        }
    }
}
