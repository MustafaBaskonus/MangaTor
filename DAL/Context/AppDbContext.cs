using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Comic> Comics { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ReactionType> ReactionTypes { get; set; }
        public DbSet<ComicRating> ComicRatings { get; set; }
        public DbSet<UserChapterReaction> UserChapterReactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<Comic>()
                    .HasMany(c => c.Categories)
                    .WithMany(c => c.Comics)
                    .UsingEntity(j => j.ToTable("ComicCategories"));
            modelBuilder.Entity<Comic>().HasData(
                new Comic()
                {
                    ComicId = 1,
                    Title = "Kingdom",
                    DateTime = DateTime.Now,
                    Slug = "kingdom"
                });

            modelBuilder.Entity<IdentityRole>().HasData(
               new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" },
               new IdentityRole() { Name = "User", NormalizedName = "USER" },
               new IdentityRole() { Name = "Editor", NormalizedName = "EDITOR" }
               );

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Comic)
                .WithMany(c => c.Comments)
                .HasForeignKey(c => c.ComicId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Chapter)
                .WithMany(ch => ch.Comments)
                .HasForeignKey(c => c.ChapterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);


            // ReactionType
            modelBuilder.Entity<ReactionType>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(r => r.Icon)
                      .HasMaxLength(50);
            });
            // UserChapterReaction
            modelBuilder.Entity<UserChapterReaction>(entity =>
                 {
                     entity.HasKey(ucr => ucr.Id);

                     entity.Property(ucr => ucr.CreatedAt)
                           .HasDefaultValueSql("GETUTCDATE()");

                     // User → UserChapterReaction (bir kullanıcının birçok tepkisi olabilir)
                     entity.HasOne(ucr => ucr.User)
                           .WithMany() // IdentityUser tarafında navigation yoksa boş bırakılır
                           .HasForeignKey(ucr => ucr.UserId)
                           .OnDelete(DeleteBehavior.Cascade);

                     // Chapter → UserChapterReaction
                     entity.HasOne(ucr => ucr.Chapter)
                           .WithMany(c => c.Reactions)
                           .HasForeignKey(ucr => ucr.ChapterId)
                           .OnDelete(DeleteBehavior.Cascade);

                     // ReactionType → UserChapterReaction
                     entity.HasOne(ucr => ucr.ReactionType)
                           .WithMany()
                           .HasForeignKey(ucr => ucr.ReactionTypeId)
                           .OnDelete(DeleteBehavior.Cascade);

                     // Bir kullanıcı bir chapter’a aynı tepkiyi sadece 1 kez verebilir
                     entity.HasIndex(ucr => new { ucr.UserId, ucr.ChapterId, ucr.ReactionTypeId })
                           .IsUnique();
                 });
        }
    }
}

