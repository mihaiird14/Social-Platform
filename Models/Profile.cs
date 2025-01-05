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
        [StringLength(50, ErrorMessage = "Descrierea nu poate avea mai mult de 50 de caractere")]
        [MinLength(5, ErrorMessage = "Descrierea trebuie sa aibă mai mult de 5 caractere")]
        public string Bio { get; set; }
        public bool ProfilPublic { get; set; } = true;
        public string ProfileImage { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Thread2>? Threads { get; set; }
        public virtual ICollection<ThreadLike>? LikedThreads { get; set; }
        public virtual ICollection<ThreadComment>? Comments { get; set; }
        public virtual ICollection<ThreadCommentsLike>? Comment_Likes { get; set; }
        public virtual ICollection<Follow>? Urmaritori { get; set; }
        public virtual ICollection<Follow>? Urmariti { get; set; }
        public virtual ICollection<Postare>? Postari { get; set; }
        public virtual ICollection<PostareLike> LikedPosts { get; set; }
        public virtual ICollection<PostsComment>?PostsComments { get; set; }
        public virtual ICollection<PostCommentsLike> Post_Com_Likes { get; set; }
        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}
