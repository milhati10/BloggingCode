using Application.Handlers.CreatePostHandler.Model;
using Application.Shared;

namespace Application.Handlers.CreatePostHandler.Interfaces
{
    public interface ICreatePostHandler
    {
        public Task<Output> ExecuteAsync(CreatePostInput input, CancellationToken cancellationToken);
    }
}
