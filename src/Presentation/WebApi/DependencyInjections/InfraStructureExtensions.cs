using Domain.Repositories;
using InfraStructure.Data.Context;
using InfraStructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DependencyInjections
{
    public static class InfraStructureExtensions
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
            => services.AddContext(configuration)
                       .AddRepositories();

        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServerBlog"));
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services.AddScoped<IPostRepository, PostRepository>();
    }
}
