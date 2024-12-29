using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Social_Life.Models
{
    public class PostareLike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostareLikeId { get; set; }
        public int PostareId { get; set; }
        public virtual Postare Postare { get; set; }
        public string ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public DateTime LikeDate { get; set; }
    }
}
