using FluentValidation;
using TAG.Models;
using TAG.Constants;

namespace Tag.Models.Validators
{
    public class PaginationValidator<T> : AbstractValidator<T>
        where T : Pagination
    {
        public PaginationValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(PaginationSettings.MIN_PAGE_NUMBER);

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(PaginationSettings.MIN_PAGE_SIZE)
                .LessThanOrEqualTo(PaginationSettings.MAX_PAGE_SIZE);
        }
    }
}
