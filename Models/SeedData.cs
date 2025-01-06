using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Social_Life.Data;

namespace Social_Life.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider
serviceProvider)
        {
            using (var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService
            <DbContextOptions<ApplicationDbContext>>()))
            {
                // Verificam daca in baza de date exista cel putin un
                //rol
                // insemnand ca a fost rulat codul
                // De aceea facem return pentru a nu insera rolurile
                //inca o data
                // Acesta metoda trebuie sa se execute o singura data
                if (context.Roles.Any())
                {
                    return; // baza de date contine deja roluri
                }
                context.Roles.AddRange(
                    new IdentityRole
                    {
                        Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                        Name = "Admin",
                        NormalizedName = "Admin".ToUpper()
                    }

                );
                var hasher = new PasswordHasher<ApplicationUser>();
                context.Users.AddRange(
                     new ApplicationUser
                     {
                         Id = "8e445865-a24d-4543-a6c6-9443d048cdb0",
                         UserName = "Admin",
                         FirstName = "Admin",
                         LastName = "Platformă",
                         DateOfBirth = new DateTime(2025, 1, 1, 0, 0, 0),
                         EmailConfirmed = true,
                         NormalizedEmail = "ADMIN@TEST.COM",
                         Email = "admin@test.com",
                         NormalizedUserName = "ADMIN",
                         PasswordHash = hasher.HashPassword(null, "Admin1!")
                     }
                );
                context.Profiles.AddRange(
                new Profile
                    {
                        Id_User = "8e445865-a24d-4543-a6c6-9443d048cdb0", // UserId al Adminului
                        Bio = "This is the admin profile.",
                        Nume = "Admin",
                        Prenume = "Platformă",
                        Username = "Admin",
                        Email = "admin@test.com",
                        DateOfBirth = new DateTime(2025, 1, 1, 0, 0, 0),
                        ProfileImage = "/imagini/pozaDefaultProfil.jpg" // Imaginea profilului
       
                    }
                );
                context.UserRoles.AddRange(
                    new IdentityUserRole<string>
                    {
                        RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                        UserId = "8e445865-a24d-4543-a6c6-9443d048cdb0"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
    
                
