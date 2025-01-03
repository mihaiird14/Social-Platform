using System.ComponentModel.DataAnnotations;

namespace Social_Life.Models
{
    public class PostsComment
    {
        [Key]
        public int PostCommentId { set; get; }
        public int PostId { set; get; }
        public virtual Postare Postare { set; get; }
        public string Id_User { set; get; }
        public virtual Profile Profile { set; get; }
        public string CommentText { set; get; }
        public DateTime Date { set; get; }
        public bool Edited { set; get; } = false;
        public int Likes { set; get; } = 0;
    }
}
