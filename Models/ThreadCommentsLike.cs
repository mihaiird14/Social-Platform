using System.ComponentModel.DataAnnotations;

namespace Social_Life.Models
{
    public class ThreadCommentsLike
    {
        [Key]
        public int ThreadCMLK_Id { get; set; }
        public int Comment_Id { get; set; }
        public virtual ThreadComment ThreadComment { get; set; }
        public string User_id { get; set; }
        public virtual Profile Profile { get; set; }
        public DateTime Data {  get; set; }
    }
}
