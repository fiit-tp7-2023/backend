using Neo4jClient;
using TAG.Nodes;
using TAG.Extensions;
using TAG.Constants;
using TAG.DTOS;
using TAG.Services.Interfaces;

namespace TAG.Services
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
                .Match("(tag:Tag)")
                .WhereIf<TagNode>(
                    request.Query != null && request.Query != "",
                    (tag) => tag.Type.Contains(request.Query)
                );

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
