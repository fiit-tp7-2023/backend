using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using TAG.Models;

namespace TAG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class Neo4jController : ControllerBase
    {
        private readonly ILogger<Neo4jController> _logger;
        private readonly IGraphClient _graphClient;

        public Neo4jController(ILogger<Neo4jController> logger, IGraphClient graphClient)
        {
            _logger = logger;
            _graphClient = graphClient;
        }

        [HttpGet("Players")]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            var players = await _graphClient.Cypher.Match("(n:PLAYER)").Return(n => n.As<Player>()).ResultsAsync;

            return Ok(players);
        }
    }
}
