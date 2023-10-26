using TAG.Constants;

namespace TAG.Models
{
    public abstract class Pagination
    {
        public int PageNumber { get; set; } = PaginationSettings.DEFAULT_PAGE_NUMBER;
        public int PageSize { get; set; } = PaginationSettings.DEFAULT_PAGE_SIZE;
    }
}
