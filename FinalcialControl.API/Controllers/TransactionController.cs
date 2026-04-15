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
        public IActionResult GetTransactions()
        {
            try
            {
                var transactions = _transactionService.GetTransactions();
                return Ok(transactions);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var transaction = _transactionService.GetById(id);
                return Ok(transaction);

            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("type/{type}")]
        public IActionResult GetByType(string type)
        {
            try
            {
                var transactions = _transactionService.GetTransactionByType(type);
                return Ok(transactions);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
            public IActionResult Create([FromBody] CreateTransactionDTO transaction)
            {
                try
                {
                    var transactionCreated = _transactionService.Add(transaction);
                    return Ok(transactionCreated);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }

            }
        }
    }
