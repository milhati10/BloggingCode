using Domain.SeedWorks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Comment(string content, DateTime createAt, int postId) : Entity
    {
        [Column("Content", TypeName = "varchar(200)")]
        [Required]
        public string Content { get; private set; } = content;

        [Column("CreatedAt", TypeName = "datetime")]
        [Required]
        public DateTime CreateAt { get; private set; } = createAt;

        public int PostId { get; private set; } = postId;
        public Post Post { get; private set; } = null!;
    }
}
