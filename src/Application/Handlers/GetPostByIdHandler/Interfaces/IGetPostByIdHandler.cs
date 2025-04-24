using Application.Shared;

namespace Application.Handlers.GetPostByIdHandler.Interfaces
{
    public interface IGetPostByIdHandler
    {
        Task<Output> ExecuteAsync(int postId, CancellationToken cancellationToken);
    }
}
