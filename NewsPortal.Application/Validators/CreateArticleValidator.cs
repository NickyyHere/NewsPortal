using FluentValidation;
using NewsPortal.Application.DTO.Create;

namespace NewsPortal.Application.Validators
{
    public class CreateArticleValidator : AbstractValidator<CreateAtricleDTO>
    {
        public CreateArticleValidator()
        {
            RuleFor(x => x.title)
                .NotEmpty()
                .WithMessage("Title is required");
            RuleFor(x => x.content)
                .NotEmpty()
                .MinimumLength(10)
                .WithMessage("Content must be at least 10 characters long");
        }
    }
}
