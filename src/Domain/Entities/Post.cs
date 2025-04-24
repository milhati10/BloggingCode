using Domain.SeedWorks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Post : Entity
    {
        [Column("Title", TypeName = "varchar(200)")]
        [Required]
        public string Title { get; private set; }

        [Column("Content", TypeName = "nvarchar(4000)")]
        [Required]
        public string Content { get; private set; }

        [Column("CreatedAt", TypeName = "datetime")]
        [Required]
        public DateTime CreatedAt { get; private set; }

        public ICollection<Comment> Comments { get; private set; } = [];

        public Post(
            string title,
            string content,
            DateTime createdAt
            )
        {
            Title = title;
            Content = content;
            CreatedAt = createdAt;
            Comments = [];
        }

        public void AddComment(string comment) { Comments.Add(new Comment(content: comment, DateTime.Now, Id)); }
    }
}
