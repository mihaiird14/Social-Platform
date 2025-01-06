using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Social_Life.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }
        [Required]
        
        public string Id_User { get; set; }
        
        public virtual Profile Profile1 { get; set; }
        [Required]
        public string Id_User2 { get; set; }
        //public virtual Profile Profile2 { get; set; }
        [Required]
        public string NotificationType { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; } = false;
        public bool Accepted { get; set; } 
        public DateTime Date { get; set; }
    }
}
