using System.ComponentModel.DataAnnotations;

namespace Social_Life.Models
{
    public class ThreadComment
    {
        [Key]
        public int ThreadCommentId { set; get; }
        public int ThreadId { set; get; }
        public virtual Thread2 Thread2 { set; get; }
        public string Id_User { set; get; }
        public virtual Profile Profile { set; get; }
        public string CommentText { set; get; }
        public DateTime Date { set; get; }
        public bool Edited { set; get; } = false;
        public int Likes { set; get; } = 0;
        public virtual ICollection<ThreadCommentsLike>? Comment_Likes { get; set; }
    }
}
