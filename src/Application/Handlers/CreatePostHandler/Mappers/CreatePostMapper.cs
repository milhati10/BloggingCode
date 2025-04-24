using Application.Handlers.CreatePostHandler.Model;
using Domain.Entities;

namespace Application.Handlers.CreatePostHandler.Mappers
{
    internal static class CreatePostMapper
    {
        public static Post MapToPost(this CreatePostInput input)
            => new(input.Title, input.Content, DateTime.UtcNow);
    }
}
