using AutoMapper;
using TAG.QueryResults;

namespace TAG.DTOS.Mappings
{
    public class NFTProfile : Profile
    {
        public NFTProfile()
        {
            CreateMap<NFTQueryResult, NFTDTO>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.NFT.Address))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NFT.Name))
                .ForMember(dest => dest.Uri, opt => opt.MapFrom(src => src.NFT.Uri))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.NFT.Description))
                .ForMember(dest => dest.AttributesString, opt => opt.MapFrom(src => src.NFT.AttributesString))
                .ForMember(
                    dest => dest.Tags,
                    opt =>
                        opt.MapFrom(
                            src =>
                                src.Tags.Zip(
                                    src.TagRelations,
                                    (tagNode, tagRelationNode) =>
                                        new TagRelationDTO { Type = tagNode.Type, Value = tagRelationNode.Value }
                                )
                        )
                );
        }
    }
}
