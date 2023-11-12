using Neo4jClient;
using TAG.Extensions;
using TAG.Constants;
using TAG.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using TAG.QueryResults;
using AutoMapper;
using TAG.Database.Relationships;
using TAG.Database.Nodes;
using TAG.DTOS;

namespace TAG.Services
{
    public class NFTService : INFTService
    {
        private readonly IGraphClient _graphClient;
        private readonly IMapper _mapper;

        public NFTService(IGraphClient graphClient, IMapper mapper)
        {
            _graphClient = graphClient;
            _mapper = mapper;
        }

        public async Task<NFTDTO> GetNFTAsync(string id)
        {
            var queryResult = await _graphClient.Cypher
                .Match($"(nft:{NodeNames.NFT})")
                .Where<NFTNode>((nft) => nft.Id == id)
                .OptionalMatch($"(nft)-[rel:{RelationshipNames.TAGGED}]->(tag:{NodeNames.TAG})")
                .ReturnDistinct(
                    (nft, rel, tag) =>
                        new NFTQueryResult
                        {
                            NFT = nft.As<NFTNode>(),
                            Tags = tag.CollectAsDistinct<TagNode>(),
                            TagRelations = rel.CollectAsDistinct<TaggedRelationship>()
                        }
                )
                .FirstOrDefaultAsync();

            return _mapper.Map<NFTDTO>(queryResult);
        }
    }
}
