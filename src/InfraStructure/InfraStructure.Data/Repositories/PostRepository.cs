using Domain.Entities;
using Domain.Repositories;
using Domain.SeedWorks;
using InfraStructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InfraStructure.Data.Repositories
{
    public class PostRepository(BlogDbContext blogDbContext) : IPostRepository
    {
        private readonly BlogDbContext _blogDbContext = blogDbContext ?? throw new ArgumentNullException(nameof(blogDbContext));
        public IUnitOfWork UnitOfWork => _blogDbContext;

        public async Task<IEnumerable<Post>> GetAllWithComments(CancellationToken cancellationToken)
        => await _blogDbContext.Posts.Include(x => x.Comments).AsNoTracking().ToListAsync(cancellationToken);

        public async Task<Post?> GetById(int postid, CancellationToken cancellationToken)
        => await _blogDbContext.Posts.Where(x => x.Id == postid).Include(x => x.Comments).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

        public async Task SaveAsync(Post post, CancellationToken cancellationToken)
        => await _blogDbContext.AddAsync(post, cancellationToken);

        public void Update(Post post)
        => _blogDbContext.Posts.Update(post);
    }
}
