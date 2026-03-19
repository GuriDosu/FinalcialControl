using FinancialControl.Models.Entities;
using FinancialControl.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace FinalcialControl.API.Controllers
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
            var transactionCreated = _transactionService;
            return Ok(transactionCreated);
        }
    }
}
