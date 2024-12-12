using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Social_Life.Models
{
    public class Profile
    {
        [Key]
        public string Id_User { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Descrierea este obligatorie")]
        public string Bio { get; set; }
        public bool ProfilPublic { get; set; } = true;
        public string ProfileImage { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Thread2>? Threads { get; set; }
        public virtual ICollection<ThreadLike>? LikedThreads { get; set; }
        public virtual ICollection<ThreadComment>? Comments { get; set; }
        public virtual ICollection<ThreadCommentsLike>? Comment_Likes { get; set; }
    }
}
