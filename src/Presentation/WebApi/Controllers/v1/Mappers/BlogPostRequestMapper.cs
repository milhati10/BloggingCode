using Application.Handlers.CreateCommentHandler.Model;
using Application.Handlers.CreatePostHandler.Model;
using WebApi.Controllers.v1.Models;

namespace WebApi.Controllers.v1.Mappers
{
    public static class BlogPostRequestMapper
    {
        public static CreatePostInput MapToInput(this BlogPostRequest input) =>
            new(input.Title, input.Content);

        public static CreateCommentInput MapToInput(this CommentRequest commentRequest, int postId)
            => new(postId, commentRequest.Comment);
    }
}
