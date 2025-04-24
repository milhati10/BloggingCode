using Application.Handlers.CreateCommentHandler.Interfaces;
using Application.Handlers.CreateCommentHandler.Model;
using Application.Shared;
using Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CreateCommentHandler
{
    public class CreateCommentHandler(ILogger<CreateCommentHandler> logger,
                                      IPostRepository postRepository,
                                      IValidator<CreateCommentInput> validator) : ICreateCommentHandler
    {
        private readonly ILogger<CreateCommentHandler> _logger = logger;
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IValidator<CreateCommentInput> _validator = validator;

        public async Task<Output> ExecuteAsync(CreateCommentInput input, CancellationToken cancellationToken)
        {
            var output = new Output();

            try
            {
                var validationResult = await _validator.ValidateAsync(input, cancellationToken);

                if (validationResult.IsValid is false)
                {
                    output.AddMessageErrors(validationResult);
                    return output;
                }

                var post = await _postRepository.GetById(input.PostId, cancellationToken);

                if (post is null)
                {
                    output.AddMessageErrors($"Post with Id {input.PostId} not found");
                    return output;
                }

                post.AddComment(input.Comment);

                _postRepository.Update(post);

                await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when try to insert a comment");

                throw;
            }
        }
    }
}
