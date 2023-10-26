using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TAG.Models;
using TAG.Services.Interfaces;

namespace TAG.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionModel?>> GetTransaction(string id)
        {
            var result = await _transactionService.GetTransactionAsync(id);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TransactionModel>>> SearchTransactions(
            [FromQuery] TransactionSearchRequest request
        )
        {
            var result = await _transactionService.SearchTransactionsAsync(request);

            return Ok(result);
        }
    }
}
