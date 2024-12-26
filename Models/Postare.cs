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
        public DateTime Data {  get; set; }
    }
}
