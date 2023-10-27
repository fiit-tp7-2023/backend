using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TAG.DTOS;
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
        public async Task<ActionResult<TransactionDTO?>> GetTransaction(string id)
        {
            var result = await _transactionService.GetTransactionAsync(id);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> SearchTransactions(
            [FromQuery] TransactionSearchRequestDTO request
        )
        {
            var result = await _transactionService.SearchTransactionsAsync(request);

            return Ok(result);
        }
    }
}
