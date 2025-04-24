using Application.Handlers.GetAllPostsHandler.Model;
using Application.Handlers.GetPostByIdHandler.Interfaces;
using Application.Handlers.GetPostByIdHandler.Model;
using Application.Shared;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
namespace Application.Handlers.GetPostByIdHandler
{
    public class GetPostByIdHandler(ILogger<GetPostByIdHandler> logger,
                                   IPostRepository postRepository) : IGetPostByIdHandler
    {
        private readonly ILogger<GetPostByIdHandler> _logger = logger;
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<Output> ExecuteAsync(int postId, CancellationToken cancellationToken)
        {
            var output = new Output();

            try
            {
                var post = await _postRepository.GetById(postId, cancellationToken);

                if (post is null)
                    return output;

                output.AddResult(CreateResponse(post));

                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when try to get a post by Id");

                throw;
            }
        }

        private GetPostByIdResponse CreateResponse(Post post)
       => new()
       {
           Title = post.Title,
           Content = post.Content,
           Id = post.Id,
           Comments = post.Comments.Select(x => new Comments { Content = x.Content })
       };
    }
}
