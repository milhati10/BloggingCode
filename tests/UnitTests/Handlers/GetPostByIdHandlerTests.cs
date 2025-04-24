using Application.Handlers.GetPostByIdHandler;
using Application.Handlers.GetPostByIdHandler.Model;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Mocks;

namespace UnitTest.Handlers
{
    public class GetPostByIdHandlerTests
    {
        private readonly WebApiMocks _webApiMocks;
        private readonly GetPostByIdHandler _handler;

        public GetPostByIdHandlerTests()
        {
            _webApiMocks = new WebApiMocks();

            _handler = new GetPostByIdHandler(_webApiMocks.MockLoggerGetPostByIdHandler.Object, _webApiMocks.MockIPostRepository.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnOutput_WhenPostExists()
        {
            // Arrange
            var post = new Post("Test Post", "Test Content", DateTime.UtcNow)
            {
                Id = 1
            };

            post.AddComment("Comment 1");
            post.AddComment("Comment 2");

            _webApiMocks.MockIPostRepository
                .Setup(repo => repo.GetById(post.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(post);

            // Act
            var result = await _handler.ExecuteAsync(post.Id, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            var response = result.Result as GetPostByIdResponse;
            Assert.NotNull(response);
            Assert.Equal(post.Id, response.Id);
            Assert.Equal(post.Title, response.Title);
            Assert.Equal(post.Content, response.Content);
            Assert.Equal(2, response.Comments.Count());
            _webApiMocks.MockIPostRepository.Verify(repo => repo.GetById(post.Id, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnEmptyOutput_WhenPostDoesNotExist()
        {
            // Arrange
            _webApiMocks.MockIPostRepository
                .Setup(repo => repo.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Post)null);

            // Act
            var result = await _handler.ExecuteAsync(1, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            _webApiMocks.MockIPostRepository.Verify(repo => repo.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var exception = new Exception("Test exception");
            _webApiMocks.MockIPostRepository
                .Setup(repo => repo.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.ExecuteAsync(1, CancellationToken.None));
        }
    }
}
