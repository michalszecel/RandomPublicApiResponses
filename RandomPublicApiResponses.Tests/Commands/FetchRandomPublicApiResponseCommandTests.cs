using FluentAssertions;
using Moq;
using RandomPublicApiResponses.Commands.FetchRandomPublicApiResponse;
using RandomPublicApiResponses.Models;
using RandomPublicApiResponses.Repositories;
using RandomPublicApiResponses.Services;
using System.Net;

namespace RandomPublicApiResponses.Tests.Commands
{
    public class FetchRandomPublicApiResponseCommandTests
    {
        private readonly FetchRandomPublicApiResponseCommandHandler _handler;
        private readonly Mock<IHttpClientService> _httpClient;

        public FetchRandomPublicApiResponseCommandTests()
        {
            _httpClient = new Mock<IHttpClientService>();

            var azureTableService = new Mock<IGenericDataTableRepository<RandomApiResponseModel>>();
            var blobStorageRepository = new Mock<IBlobStorageRepository>();

            _handler = new FetchRandomPublicApiResponseCommandHandler(_httpClient.Object, azureTableService.Object, blobStorageRepository.Object);
        }

        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.NotImplemented)]
        [InlineData(HttpStatusCode.NotFound)]
        [InlineData(HttpStatusCode.InternalServerError)]
        public void Status_Code_Correct(HttpStatusCode statusCode)
        {
            // Arrange
            _httpClient.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(new HttpResponseMessage(statusCode)
            {
                Content = new StringContent("test")
            });

            // Act
            var result = _handler.Handle(new FetchRandomPublicApiResponseCommand(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Result.ContentFilename.Should().NotBeNull();
            result.Result.PartitionKey.Should().NotBeNull();
            result.Result.RowKey.Should().NotBeNull();
            result.Result.StatusCode.Should().Be(statusCode);
            result.Result.Exception.Should().BeNull();
        }

        [Fact]
        public void Exception_Should_Be_Stored()
        {
            // Arrange
            var exceptionMessage = "test message";
            var exception = new Exception(exceptionMessage);
            _httpClient.Setup(x => x.GetAsync(It.IsAny<string>())).Throws(exception);

            // Act
            var result = _handler.Handle(new FetchRandomPublicApiResponseCommand(), CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Result.ContentFilename.Should().BeNull();
            result.Result.PartitionKey.Should().NotBeNull();
            result.Result.RowKey.Should().NotBeNull();
            result.Result.Exception.Should().NotBeNull();
        }
    }
}