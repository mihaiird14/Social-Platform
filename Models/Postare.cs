using System.ComponentModel.DataAnnotations;

namespace Social_Life.Models
{
    public class Postare
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual Profile Profile { get; set; }
        [Required(ErrorMessage ="Imaginea este obligatorie!")]
        public string Imagine {  get; set; }
        [StringLength(50,ErrorMessage ="Descrierea trebuie sa aiba maxim 50 caractere!")]
        public string Descriere { get; set; }
        public virtual ICollection <PostareLike2> PostareLike { get; set; }
        public int NrLikePostare { set; get; } = 0;
        public DateTime Data {  get; set; }
        public int NrComentarii { get; set; } = 0;
        public virtual ICollection<PostsComment>? Comments { get; set; }
    }
}
