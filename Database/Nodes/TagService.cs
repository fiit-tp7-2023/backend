using Neo4jClient;
using TAG.Extensions;
using TAG.Constants;
using TAG.DTOS;
using TAG.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace TAG.Database.Nodes
{
    public class TagService : ITagService
    {
        private readonly IGraphClient _graphClient;

        public TagService(IGraphClient graphClient)
        {
            _graphClient = graphClient;
        }

        public async Task<TagSearchResponseDTO> SearchTagsAsync(TagSearchRequestDTO request)
        {
            var query = _graphClient.Cypher
                .Match($"(tag:{NodeNames.TAG})")
                .WhereIf<TagNode>(!request.Name.IsNullOrEmpty(), (tag) => tag.Type.Contains(request.Name!));

            var pageCount = (int)
                Math.Ceiling(await query.Return((tag) => tag.Count()).FirstOrDefaultAsync() / (double)request.PageSize);

            var tags = await query
                .ReturnDistinct((tag) => tag.As<TagNode>().Type)
                .OrderBy("tag.type")
                .Paginate(request.PageNumber, request.PageSize)
                .ResultsAsync;

            return new TagSearchResponseDTO { Tags = tags, PageCount = pageCount };
        }
    }
}
