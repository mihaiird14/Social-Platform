using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Life.Models
{
    public class Follow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FollowId { get; set; }
        [Required]
        public string Id_Urmaritor { get; set; }
        [Required]
        public string Id_Urmarit { get; set; }
        public virtual Profile Urmaritor { get; set; }
        public virtual Profile Urmarit { get; set; }
        [Required]
        public DateTime Data { get; set; }
    }
}