using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Life.Models
{
    public class Thread2
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThreadId { get; set; }
        [Required]
        public string Id_User { set; get; }
        [Required(ErrorMessage = "Textul este obligatoriu!")]
        [MaxLength(450,ErrorMessage="Textul depaseste dimensiunea maxima!")]
        public string ThreadText { get; set; }
        public int ThreadLikes { get; set; } = 0;
        public int ThreadComments { get; set; } = 0;
        public DateTime Date { get; set; }
        public bool Edited { get; set; } = false;
        public virtual Profile Profile { set; get; }
        public virtual ICollection<ThreadLike>? Likes { get; set; }
        public virtual ICollection<ThreadComment>? Comments { get; set; }
    }
}
