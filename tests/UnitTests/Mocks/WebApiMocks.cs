using Application.Handlers.CreateCommentHandler;
using Application.Handlers.CreateCommentHandler.Model;
using Application.Handlers.CreatePostHandler;
using Application.Handlers.CreatePostHandler.Model;
using Application.Handlers.GetAllPostsHandler;
using Application.Handlers.GetPostByIdHandler;
using Domain.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTest.Mocks
{
    public class WebApiMocks
    {
        public Mock<ILogger<CreateCommentHandler>> MockLoggerCreateCommentHandler;
        public Mock<ILogger<CreatePostHandler>> MockLoggerCreatePostHandler;
        public Mock<ILogger<GetAllPostsHandler>> MockLoggerGetAllPostsHandler;
        public Mock<ILogger<GetPostByIdHandler>> MockLoggerGetPostByIdHandler;

        public Mock<IValidator<CreateCommentInput>> MockValidatorCreateCommentInput;
        public Mock<IValidator<CreatePostInput>> MockValidatorCreatePostInput;
        
        public Mock<IPostRepository> MockIPostRepository;

        public WebApiMocks()
        {
            MockLoggerCreateCommentHandler = new Mock<ILogger<CreateCommentHandler>>();
            MockLoggerCreatePostHandler = new Mock<ILogger<CreatePostHandler>>();
            MockLoggerGetAllPostsHandler = new Mock<ILogger<GetAllPostsHandler>>();
            MockLoggerGetPostByIdHandler = new Mock<ILogger<GetPostByIdHandler>>();

            MockValidatorCreateCommentInput = new Mock<IValidator<CreateCommentInput>>();
            MockValidatorCreatePostInput = new Mock<IValidator<CreatePostInput>>();

            MockIPostRepository = new Mock<IPostRepository>();
        }
    }
}
