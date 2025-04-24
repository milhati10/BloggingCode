using Application.Shared;

namespace Application.Handlers.GetAllPostsHandler.Interfaces
{
    public interface IGetAllPostsHandler
    {
        Task<Output> ExecuteAsync(CancellationToken cancellationToken);
    }
}
