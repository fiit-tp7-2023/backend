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
                .Match($"(nft:{NodeNames.NFT})-[rel:{RelationshipNames.TAGGED}]->(tag:{NodeNames.TAG})")
                .Where<NFTNode>((nft) => nft.Id == id)
                .Return(
                    (nft, rel, tag) =>
                        new NFTQueryResult
                        {
                            NFT = nft.As<NFTNode>(),
                            Tags = tag.CollectAs<TagNode>(),
                            TagRelations = rel.CollectAs<TaggedRelationship>()
                        }
                )
                .FirstOrDefaultAsync();

            return _mapper.Map<NFTDTO>(queryResult);
        }
    }
}
