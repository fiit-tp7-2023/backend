using TAG.DTOS;

namespace TAG.Services.Interfaces
{
    public interface ITagService
    {
        Task<TagSearchResponseDTO> SearchTagsAsync(TagSearchRequestDTO request);
    }
}
