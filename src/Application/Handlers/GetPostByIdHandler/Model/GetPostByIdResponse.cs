namespace Application.Handlers.GetPostByIdHandler.Model
{
    public class GetPostByIdResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public IEnumerable<Comments> Comments { get; set; }
    }

    public class Comments
    {
        public string Content { get; set; }
    }
}
