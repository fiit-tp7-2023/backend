using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using TAG.DTOS;

namespace Tag.DTOS.Validators
{
    public class TransactionSearchRequestDTOValidator : PaginationRequestDTOValidator<TransactionSearchRequestDTO>
    {
        public TransactionSearchRequestDTOValidator()
        {
            When(
                x => !x.TagNames.IsNullOrEmpty(),
                () =>
                {
                    RuleForEach(x => x.TagNames).NotEmpty();
                }
            );
        }
    }
}
