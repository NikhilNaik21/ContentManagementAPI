using ArticleManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticleManagementAPI.Data 
{
    public class ArticleManagementDbContext: DbContext
    {
        public ArticleManagementDbContext(

              DbContextOptions<ArticleManagementDbContext> options)
            : base(options)
        {
            
        }



        public DbSet<Users> Users { get; set; }

        public DbSet<Articles> Articles { get; set; }

        public DbSet<Contents> Contents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // User -> Content
            modelBuilder.Entity<Contents>()
                .HasOne(c => c.Author)
                .WithMany(u => u.Contents)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);



            // Article -> Content
            modelBuilder.Entity<Contents>()
                .HasOne(c => c.Article)
                .WithMany(a => a.Contents)
                .HasForeignKey(c => c.ArticleId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
