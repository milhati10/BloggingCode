using Application.Handlers.GetAllPostsHandler.Interfaces;
using Application.Handlers.GetAllPostsHandler.Model;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Handlers.GetAllPostsHandler
{
    public class GetAllPostsHandler(ILogger<GetAllPostsHandler> logger,
                                    IPostRepository postRepository) : IGetAllPostsHandler
    {
        private readonly ILogger<GetAllPostsHandler> _logger = logger;
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<Output> ExecuteAsync(CancellationToken cancellationToken)
        {
            var output = new Output();

            try
            {
                var posts = await _postRepository.GetAllWithComments(cancellationToken);

                output.AddResult(CreateResponse(posts));

                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when try to get all posts");

                throw;
            }
        }

        private IEnumerable<GetAllPostsResponse> CreateResponse(IEnumerable<Post> posts)
        => posts.Select(p => new GetAllPostsResponse
        {
            Id = p.Id,
            Title = p.Title,
            Content = p.Content,
            QuantityComments = p.Comments.Count(),
        });
    }
}
