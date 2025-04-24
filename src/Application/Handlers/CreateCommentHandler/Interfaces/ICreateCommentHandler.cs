using Application.Handlers.CreateCommentHandler.Model;
using Application.Shared;

namespace Application.Handlers.CreateCommentHandler.Interfaces
{
    public interface ICreateCommentHandler
    {
        Task<Output> ExecuteAsync(CreateCommentInput input, CancellationToken cancellationToken);
    }
}
