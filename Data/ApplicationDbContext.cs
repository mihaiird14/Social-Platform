using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Social_Life.Models;
using System.Reflection.Emit;


namespace Social_Life.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Thread2> Threads { get; set; }
        public DbSet<ThreadLike> ThreadLikes { get; set; }
        public DbSet<ThreadComment> ThreadComments { get; set; }
        public DbSet<ThreadCommentsLike> ThreadCommentsLikes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<Profile>(entity =>
            {
                entity.HasKey(p => p.Id_User); 
                entity.Property(p => p.Id_User)
                      .ValueGeneratedNever(); 
            });
           builder.Entity<Thread2>()
                .Property(t => t.ThreadId)
                .ValueGeneratedOnAdd();
            builder.Entity<Profile>()
                .HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.Id_User)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Thread2>()
                .HasOne(t => t.Profile)         
                .WithMany(p => p.Threads)       
                .HasForeignKey(t => t.Id_User)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ThreadLike>()
            .HasOne(tl => tl.Thread)
            .WithMany(t => t.Likes)
            .HasForeignKey(tl => tl.ThreadId);

            builder.Entity<ThreadLike>()
                .HasOne(tl => tl.Profile)
                .WithMany(p => p.LikedThreads)
                .HasForeignKey(tl => tl.ProfileId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ThreadComment>()
                .HasOne(t => t.Thread2)
                .WithMany(c => c.Comments)
                .HasForeignKey(t => t.ThreadId);
            builder.Entity<ThreadComment>()
                .HasOne(p=>p.Profile)
                .WithMany(c=>c.Comments)
                .HasForeignKey(p=>p.Id_User)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ThreadCommentsLike>()
                .HasOne(tl => tl.Profile)
                .WithMany(t => t.Comment_Likes)
                .HasForeignKey(tl => tl.User_id);
                

            builder.Entity<ThreadCommentsLike>()
                .HasOne(tl => tl.ThreadComment)
                .WithMany(p => p.Comment_Likes)
                .HasForeignKey(tl => tl.Comment_Id);
                
        }
    }
}
