using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.v1.Models
{
    public class BlogPostRequest
    {
        [SwaggerSchema(Description = "Title of Post to Blog")]
        public required string Title { get; set; }

        [SwaggerSchema(Description = "Body of Post to Blog")]
        public required string Content { get; set; }
    }
}
