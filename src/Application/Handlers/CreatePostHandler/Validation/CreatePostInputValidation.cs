using Application.Handlers.CreatePostHandler.Model;
using FluentValidation;

namespace Application.Handlers.CreatePostHandler.Validation
{
    public class CreatePostInputValidation : AbstractValidator<CreatePostInput>
    {
        public CreatePostInputValidation()
        {
            RuleFor(x => x.Title)
                   .MaximumLength(200)
                   .NotEmpty()
                   .NotNull();

            RuleFor(x => x.Content)
                   .MaximumLength(4000)
                   .NotEmpty()
                   .NotNull();
        }
    }
}
