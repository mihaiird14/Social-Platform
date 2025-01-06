using System.ComponentModel.DataAnnotations;

namespace Social_Life.Models
{
    public class PostCommentsLike
    {
        [Key]
        public int PostCMLK_Id { get; set; }
        public int Comment_Id { get; set; }
        public virtual PostsComment PostsComment { get; set; }
        public string User_id { get; set; }
        public virtual Profile Profile { get; set; }
        public DateTime Data { get; set; }
    }
}
