using TAG.Constants;

namespace TAG.DTOS
{
    public abstract class PaginationDTO
    {
        public int PageNumber { get; set; } = PaginationSettings.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PaginationSettings.DEFAULT_PAGE_SIZE;
    }
}
