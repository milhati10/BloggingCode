using Application.Handlers.GetAllPostsHandler;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTest.Mocks;

namespace UnitTest.Handlers
{
    public class GetAllPostsHandlerTests
    {
        private readonly WebApiMocks _mocks;
        private readonly GetAllPostsHandler _getAllPostsHandler;

        public GetAllPostsHandlerTests()
        {
            _mocks = new WebApiMocks();
            _getAllPostsHandler = new GetAllPostsHandler(_mocks.MockLoggerGetAllPostsHandler.Object, _mocks.MockIPostRepository.Object);
        }


        [Fact]
        public async Task ExecuteAsync_ShouldReturnOutput_WhenPostsAreAvailable()
        {
            // Arrange
            var posts = new List<Post>{
                new(title: "Title1", content: "Content1", DateTime.Now),
                new(title : "Title2", content : "Content2", DateTime.Now)
            };

            _mocks.MockIPostRepository
                .Setup(repo => repo.GetAllWithComments(It.IsAny<CancellationToken>()))
                .ReturnsAsync(posts);

            // Act
            var result = await _getAllPostsHandler.ExecuteAsync(CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            _mocks.MockIPostRepository.Verify(repo => repo.GetAllWithComments(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var exception = new Exception("Test exception");

            _mocks.MockIPostRepository
                .Setup(repo => repo.GetAllWithComments(It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _getAllPostsHandler.ExecuteAsync(CancellationToken.None));
        }

    }
}
