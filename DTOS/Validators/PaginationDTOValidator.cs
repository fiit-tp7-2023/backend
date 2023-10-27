using FluentValidation;
using TAG.DTOS;
using TAG.Constants;

namespace Tag.DTOS.Validators
{
    public class PaginationDTOValidator<T> : AbstractValidator<T>
        where T : PaginationDTO
    {
        public PaginationDTOValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(PaginationSettings.MIN_PAGE_NUMBER);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(PaginationSettings.MIN_PAGE_SIZE)
                .LessThanOrEqualTo(PaginationSettings.MAX_PAGE_SIZE);
        }
    }
}
