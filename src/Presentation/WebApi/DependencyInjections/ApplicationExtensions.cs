using Application.Handlers.CreateCommentHandler;
using Application.Handlers.CreateCommentHandler.Interfaces;
using Application.Handlers.CreateCommentHandler.Model;
using Application.Handlers.CreateCommentHandler.Validators;
using Application.Handlers.CreatePostHandler;
using Application.Handlers.CreatePostHandler.Interfaces;
using Application.Handlers.CreatePostHandler.Model;
using Application.Handlers.CreatePostHandler.Validation;
using Application.Handlers.GetAllPostsHandler;
using Application.Handlers.GetAllPostsHandler.Interfaces;
using Application.Handlers.GetPostByIdHandler;
using Application.Handlers.GetPostByIdHandler.Interfaces;
using FluentValidation;

namespace WebApi.DependencyInjections
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddValidators()
                       .AddHandlers();

        public static IServiceCollection AddHandlers(this IServiceCollection services) =>
            services.AddScoped<ICreateCommentHandler, CreateCommentHandler>()
                    .AddScoped<ICreatePostHandler, CreatePostHandler>()
                    .AddScoped<IGetAllPostsHandler, GetAllPostsHandler>()
                    .AddScoped<IGetPostByIdHandler, GetPostByIdHandler>();

        public static IServiceCollection AddValidators(this IServiceCollection services) =>
            services.AddScoped<IValidator<CreatePostInput>, CreatePostInputValidation>()
                    .AddScoped<IValidator<CreateCommentInput>, CreateCommentValidators>();
    }
}
