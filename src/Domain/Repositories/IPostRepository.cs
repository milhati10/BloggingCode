using Domain.Entities;
using Domain.SeedWorks;

namespace Domain.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task SaveAsync(Post post, CancellationToken cancellationToken);

        Task<Post?> GetById(int postid,CancellationToken cancellationToken);

        Task<IEnumerable<Post>> GetAllWithComments(CancellationToken cancellationToken);

        void Update(Post post);
    }
}
