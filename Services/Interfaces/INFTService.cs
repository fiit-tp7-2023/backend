using TAG.DTOS;

namespace TAG.Services.Interfaces
{
    public interface INFTService
    {
        Task<NFTDTO> GetNFTAsync(string address);
    }
}
