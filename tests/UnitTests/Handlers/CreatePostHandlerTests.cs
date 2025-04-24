using Application.Handlers.CreatePostHandler;
using Application.Handlers.CreatePostHandler.Interfaces;
using Application.Handlers.CreatePostHandler.Model;
using Domain.Entities;
using Domain.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using UnitTest.Mocks;
using Xunit;

namespace UnitTest.Handlers
{
    public class CreatePostHandlerTests
    {
        private readonly WebApiMocks _mocks;

        private readonly CreatePostHandler _handler;

        public CreatePostHandlerTests()
        {
            _mocks = new WebApiMocks();
            _handler = new CreatePostHandler(_mocks.MockLoggerCreatePostHandler.Object, _mocks.MockValidatorCreatePostInput.Object, _mocks.MockIPostRepository.Object);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnOutputWithErrors_WhenInputIsInvalid()
        {
            // Arrange
            var input = new CreatePostInput("","" );
            var validationResult = new ValidationResult(new[] { new ValidationFailure("Title", "Title is required") });

            _mocks.MockValidatorCreatePostInput
                .Setup(validator => validator.ValidateAsync(input, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            // Act
            var result = await _handler.ExecuteAsync(input, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsInvalid);
            _mocks.MockIPostRepository.Verify(repo => repo.SaveAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()), Times.Never);
            _mocks.MockIPostRepository.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldSavePost_WhenInputIsValid()
        {
            // Arrange
            var input = new CreatePostInput("Valid Title", "Valid Content" );
            var validationResult = new ValidationResult(); // Valid input, no errors

            _mocks.MockValidatorCreatePostInput
                .Setup(validator => validator.ValidateAsync(input, It.IsAny<CancellationToken>()))
                .ReturnsAsync(validationResult);

            _mocks.MockIPostRepository
                .Setup(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.Run(() => 1 ));

            // Act
            var result = await _handler.ExecuteAsync(input, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsInvalid);
            _mocks.MockIPostRepository.Verify(repo => repo.SaveAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()), Times.Once);
            _mocks.MockIPostRepository.Verify(repo => repo.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldLogError_WhenExceptionIsThrown()
        {
            // Arrange
            var input = new CreatePostInput ("Test", "Content");
            var exception = new Exception("Test exception");

            _mocks.MockValidatorCreatePostInput
                .Setup(validator => validator.ValidateAsync(input, It.IsAny<CancellationToken>()))
                .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.ExecuteAsync(input, CancellationToken.None));
        }
    }
}
