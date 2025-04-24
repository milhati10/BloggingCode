using IntegratedTests.Shared;
using System.Text;
using System.Text.Json;
using WebApi.Controllers.v1.Models;

namespace IntegratedTests.WebApi
{
    public class BlogPostsControllersTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;

        public BlogPostsControllersTests(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

        [Fact]
        public async Task ExecuteAsync_CreatePost_ShouldReturnOutputWithErrors_WhenInputIsValid()
        {
            var model = new BlogPostRequest() { Content = "content example", Title = "Title" };

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/v1/posts")
            {
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(httpRequestMessage);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
