using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Build.Framework;
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
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Postare> Postari { get; set; }
        public DbSet<PostareLike> PostareLikes { get; set; }
        public DbSet<PostsComment> PostsComments { get; set; }
        public DbSet<PostCommentsLike> PostCommentsLikes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
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
            builder.Entity<PostareLike>()
                 .Property(t => t.PostareLikeId)
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
                .HasOne(p => p.Profile)
                .WithMany(c => c.Comments)
                .HasForeignKey(p => p.Id_User)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ThreadCommentsLike>()
                .HasOne(tl => tl.Profile)
                .WithMany(t => t.Comment_Likes)
                .HasForeignKey(tl => tl.User_id);


            builder.Entity<ThreadCommentsLike>()
                .HasOne(tl => tl.ThreadComment)
                .WithMany(p => p.Comment_Likes)
                .HasForeignKey(tl => tl.Comment_Id);

            builder.Entity<Follow>()
                .HasOne(f => f.Urmaritor)
               .WithMany(p => p.Urmariti)
                .HasForeignKey(f => f.Id_Urmaritor)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Follow>()
                .HasOne(f => f.Urmarit)
                .WithMany(p => p.Urmaritori)
                .HasForeignKey(f => f.Id_Urmarit)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Postare>()
                .HasOne(p => p.Profile)
                .WithMany(f => f.Postari)
                .HasForeignKey(p => p.UserId);
            builder.Entity<PostareLike>()
                .HasOne(p => p.Postare)
                .WithMany(t => t.PostareLike)
                .HasForeignKey(p => p.PostareLikeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PostareLike>()
                .HasOne(tl => tl.Profile)
                .WithMany(p => p.LikedPosts)
                .HasForeignKey(p => p.ProfileId);
        builder.Entity<PostsComment>()
                .HasOne(t=>t.Profile)
                .WithMany(p=>p.PostsComments)
                .HasForeignKey(t=>t.Id_User);
            builder.Entity<PostsComment>()
                .HasOne(t=>t.Postare)
                .WithMany(t=>t.Comments)
                .HasForeignKey(t=>t.PostId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PostCommentsLike>()
                .HasOne(tl => tl.Profile)
                .WithMany(t => t.Post_Com_Likes)
                .HasForeignKey(tl => tl.User_id);
                


            builder.Entity<PostCommentsLike>()
                .HasOne(tl => tl.PostsComment)
                .WithMany(p => p.Post_Comment_Likes)
                .HasForeignKey(tl => tl.Comment_Id)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Notification>()
                .HasOne(n => n.Profile1)
                .WithMany(p => p.Notifications)
                .HasForeignKey(n => n.Id_User)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
