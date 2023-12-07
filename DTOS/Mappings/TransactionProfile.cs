using AutoMapper;
using TAG.QueryResults;

namespace TAG.DTOS.Mappings
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionQueryResult, TransactionDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Transaction.Id))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Transaction.Amount))
                .ForMember(
                    dest => dest.NFT,
                    opt =>
                        opt.MapFrom(
                            src =>
                                new NFTDTO
                                {
                                    Address = src.NFT.Address,
                                    AnimationUrl = src.NFT.AnimationUrl,
                                    CreatedAtBlock = src.NFT.CreatedAtBlock,
                                    ExternalUrl = src.NFT.ExternalUrl,
                                    Image = src.NFT.Image,
                                    TokenId = src.NFT.TokenId,
                                    Name = src.NFT.Name,
                                    Uri = src.NFT.Uri,
                                    Description = src.NFT.Description,
                                    AttributesString = src.NFT.AttributesString,
                                    Tags = src.Tags.Zip(
                                        src.TagRelations,
                                        (tagNode, tagRelationNode) =>
                                            new TagRelationDTO { Type = tagNode.Type, Value = tagRelationNode.Value }
                                    )
                                }
                        )
                );
        }
    }
}
