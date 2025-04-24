using Application.Handlers.CreatePostHandler.Interfaces;
using Application.Handlers.CreatePostHandler.Mappers;
using Application.Handlers.CreatePostHandler.Model;
using Application.Shared;
using Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.CreatePostHandler
{
    public class CreatePostHandler(ILogger<CreatePostHandler> logger,
                                IValidator<CreatePostInput> validator,
                                IPostRepository postRepository) : ICreatePostHandler
    {
        private readonly ILogger<CreatePostHandler> _logger = logger;
        private readonly IValidator<CreatePostInput> _validator = validator;
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<Output> ExecuteAsync(CreatePostInput input, CancellationToken cancellationToken)
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

                await _postRepository.SaveAsync(input.MapToPost(), cancellationToken);

                await _postRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when try to create a post");

                throw;
            }
        }
    }
}
