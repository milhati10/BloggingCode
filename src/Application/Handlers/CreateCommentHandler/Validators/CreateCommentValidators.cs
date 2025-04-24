using Application.Handlers.CreateCommentHandler.Model;
using FluentValidation;

namespace Application.Handlers.CreateCommentHandler.Validators
{
    public class CreateCommentValidators : AbstractValidator<CreateCommentInput>
    {
        public CreateCommentValidators()
        {
            RuleFor(x => x.Comment)
                    .MaximumLength(200)
                    .NotEmpty();

            RuleFor(x => x.PostId)
                   .GreaterThan(0)
                   .NotNull();
        }
    }
}
