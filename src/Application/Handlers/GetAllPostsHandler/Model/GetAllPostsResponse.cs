namespace Application.Handlers.GetAllPostsHandler.Model
{
    public class GetAllPostsResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public int QuantityComments { get; set; }
    }
}
