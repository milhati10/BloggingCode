using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers.v1.Models
{
    public class CommentRequest
    {
        [SwaggerSchema(Description = "Comment text")]
        public required string Comment { get; set; }
    }
}
