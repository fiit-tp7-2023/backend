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
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.NFT.Image))
                .ForMember(dest => dest.ExternalUrl, opt => opt.MapFrom(src => src.NFT.ExternalUrl))
                .ForMember(dest => dest.AnimationUrl, opt => opt.MapFrom(src => src.NFT.AnimationUrl))
                .ForMember(dest => dest.CreatedAtBlock, opt => opt.MapFrom(src => src.NFT.CreatedAtBlock))
                .ForMember(dest => dest.TokenId, opt => opt.MapFrom(src => src.NFT.TokenId))
                .ForMember(dest => dest.Raw, opt => opt.MapFrom(src => src.NFT.Raw))
                .ForMember(dest => dest.NFTVector, opt => opt.MapFrom(src => src.NFT.NFTVector))
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
