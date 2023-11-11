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
                                    Id = src.NFT.Id,
                                    Name = src.NFT.Name,
                                    Uri = src.NFT.Uri,
                                    Description = src.NFT.Description,
                                    AttributesString = src.NFT.AttributesString,
                                    Tags = src.Tags.Select(tag => tag.Type)
                                }
                        )
                );
        }
    }
}
