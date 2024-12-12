using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Life.Models
{
    public class ThreadLike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThreadLikeId { get; set; }
        public int ThreadId { get; set; } 
        public Thread2 Thread { get; set; }
        public string ProfileId { get; set; } 
        public Profile Profile { get; set; }
        public DateTime LikeDate { get; set; }
    }
}
