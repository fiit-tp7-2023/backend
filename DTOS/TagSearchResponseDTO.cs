namespace TAG.DTOS
{
    public class TagSearchResponseDTO : PaginationResponseDTO
    {
        public IEnumerable<string> Tags { get; set; } = null!;
    }
}
