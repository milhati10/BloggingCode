using Microsoft.AspNetCore.Mvc;
using SerilogTimings;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Controllers.v1.Models;
using Serilog;
using Application.Handlers.CreateCommentHandler.Interfaces;
using Application.Handlers.CreatePostHandler.Interfaces;
using Application.Handlers.GetAllPostsHandler.Interfaces;
using Application.Handlers.GetPostByIdHandler.Interfaces;
using WebApi.Controllers.v1.Mappers;

namespace WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/posts")]
    public class BlogPostsControllers(ILogger<BlogPostsControllers> logger,
                                      ICreateCommentHandler createCommentHandler,
                                      ICreatePostHandler createPostHandler,
                                      IGetAllPostsHandler getAllPostsHandler,
                                      IGetPostByIdHandler getPostByIdHandler
                                        ) : ControllerBase
    {
        private readonly ILogger<BlogPostsControllers> _logger = logger;
        private readonly ICreateCommentHandler _createCommentHandler = createCommentHandler;
        private readonly ICreatePostHandler _createPostHandler = createPostHandler;
        private readonly IGetAllPostsHandler _getAllPostsHandler = getAllPostsHandler;
        private readonly IGetPostByIdHandler _getPostByIdHandler = getPostByIdHandler;

        [HttpGet()]
        [SwaggerOperation(Summary = "Get All Post and Comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPostsAndCommentsAsync(CancellationToken cancellationToken)
        {
            using var operation = Operation.Begin("{method} executed", nameof(GetAllPostsAndCommentsAsync));

            try
            {
                var output = await _getAllPostsHandler.ExecuteAsync(cancellationToken);

                if (output.IsEmptyResult)
                    return NoContent();

                operation.Complete();

                return Ok(output);
            }
            catch (Exception ex)
            {
                operation.SetException(ex).Abandon();
                throw;
            }
        }

        [HttpGet("{postId}")]
        [SwaggerOperation(Summary = "Get Post and comments by Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByPostIdAsync(int postId, CancellationToken cancellationToken)
        {
            using var operation = Operation.Begin("{method} executed", nameof(GetByPostIdAsync));

            try
            {
                var output = await _getPostByIdHandler.ExecuteAsync(postId, cancellationToken);

                if (output.IsEmptyResult)
                    return NoContent();

                operation.Complete();

                return Ok(output);
            }
            catch (Exception ex)
            {
                operation.SetException(ex).Abandon();
                throw;
            }
        }

        [HttpPost()]
        [SwaggerOperation(Summary = "Create a blog post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePostBlogAsync(BlogPostRequest model, CancellationToken cancellationToken)
        {
            using var operation = Operation.Begin("{method} executed", nameof(CreatePostBlogAsync));

            try
            {
                var input = model.MapToInput();

                var output = await _createPostHandler.ExecuteAsync(input, cancellationToken);

                if (output.IsInvalid)
                    return BadRequest(output);

                operation.Complete();
                return Ok(output);
            }
            catch (Exception ex)
            {
                operation.SetException(ex).Abandon();
                throw;
            }
        }

        [HttpPost("{postId}/comments")]
        [SwaggerOperation(Summary = "Include a comment in post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCommentAsync([FromRoute] int postId, 
                                                            [FromBody] CommentRequest commentRequest, 
                                                            CancellationToken cancellationToken)
        {
            using var operation = Operation.Begin("{method} executed", nameof(CreateCommentAsync));

            try
            {
                var input = commentRequest.MapToInput(postId);

                var output = await _createCommentHandler.ExecuteAsync(input, cancellationToken);

                operation.Complete();
                return Ok();
            }
            catch (Exception ex)
            {
                operation.SetException(ex).Abandon();
                throw;
            }
        }
    }
}
