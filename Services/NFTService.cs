using Neo4jClient;
using TAG.Nodes;
using TAG.Extensions;
using TAG.Constants;
using TAG.DTOS;
using TAG.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using TAG.QueryResults;
using AutoMapper;

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
                .Match($"(nft:{NodeNames.NFT})-[:{RelationshipNames.TAGGED}]->(tag:{NodeNames.TAG})")
                .Where<NFTNode>((nft) => nft.Id == id)
                .Return((nft, tag) => new NFTQueryResult { NFT = nft.As<NFTNode>(), Tags = tag.CollectAs<TagNode>(), })
                .FirstOrDefaultAsync();

            return _mapper.Map<NFTDTO>(queryResult);
        }
    }
}
