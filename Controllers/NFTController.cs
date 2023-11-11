using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TAG.DTOS;
using TAG.Services.Interfaces;

namespace TAG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NFTController : ControllerBase
    {
        private readonly INFTService _nftService;

        public NFTController(INFTService nftService)
        {
            _nftService = nftService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NFTDTO?>> GetNFT(string id)
        {
            var result = await _nftService.GetNFTAsync(id);

            return Ok(result);
        }
    }
}
