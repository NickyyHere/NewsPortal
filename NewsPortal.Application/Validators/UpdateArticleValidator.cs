using FluentValidation;
using NewsPortal.Application.DTO.Update;

namespace NewsPortal.Application.Validators
{
    public class UpdateArticleValidator : AbstractValidator<UpdateArticleDTO>
    {
        public UpdateArticleValidator()
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
