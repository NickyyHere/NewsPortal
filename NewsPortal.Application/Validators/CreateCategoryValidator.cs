using FluentValidation;
using NewsPortal.Application.DTO.Create;

namespace NewsPortal.Application.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty();
        }
    }
}
