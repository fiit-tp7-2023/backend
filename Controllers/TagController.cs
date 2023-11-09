using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TAG.DTOS;
using TAG.Services.Interfaces;

namespace TAG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<ActionResult<TagSearchResponseDTO>> SearchTransactions(
            [FromQuery] TagSearchRequestDTO request
        )
        {
            var result = await _tagService.SearchTagsAsync(request);

            return Ok(result);
        }
    }
}
