using ArticlesWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticlesWebApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<ArticalFavorites> ArticalFavorites { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleTags> ArticleTags { get; set; }
        public DbSet<Coments> Coments { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleTags>()
                .HasKey(at => new { at.ArticleId, at.TagId });
            // Зовнішній ключ для ArticleTags.ArticleId, посилаючись на Article.Id
            modelBuilder.Entity<ArticleTags>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticleTags)
                .HasForeignKey(at => at.ArticleId)
                .OnDelete(DeleteBehavior.NoAction);
            // Зовнішній ключ для ArticleTags.TagId, посилаючись на Tag.Id
            modelBuilder.Entity<ArticleTags>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.ArticleTags)
                .HasForeignKey(at => at.TagId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<ArticalFavorites>()
               .HasKey(at => new { at.ArticleId, at.UsertId });
            // Зовнішній ключ для ArticalFavorites.ArticleId, посилаючись на Article.Id
            modelBuilder.Entity<ArticalFavorites>()
                .HasOne(at => at.Article)
                .WithMany(a => a.ArticalFavorites)
                .HasForeignKey(at => at.ArticleId)
                .OnDelete(DeleteBehavior.NoAction);
            // Зовнішній ключ для ArticleTags.TagId, посилаючись на User.Id
            modelBuilder.Entity<ArticalFavorites>()
                .HasOne(at => at.User)
                .WithMany(t => t.ArticalFavorites)
                .HasForeignKey(at => at.UsertId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
