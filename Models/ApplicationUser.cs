using Microsoft.AspNetCore.Identity;
namespace Social_Life.Models
{
    
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual Profile Profile { get; set; }
    }

}
