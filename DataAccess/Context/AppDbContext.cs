using Core.Entities.Concrete;
using Core.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole,Guid>
    {
        public AppDbContext()
        {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GameCategory>()
                .HasKey(gc => new { gc.GameId, gc.CategoryId });

            modelBuilder.Entity<LibraryGames>()
                .HasOne(lg => lg.Game)
                .WithMany(g => g.LibraryGames)
                .HasForeignKey(lg => lg.GameId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LibraryGames>()
                .HasOne(lg => lg.Library)
                .WithMany(l => l.LibraryGames)
                .HasForeignKey(lg => lg.LibraryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<LibraryGames>()
                .HasKey(lg => new { lg.LibraryId,lg.GameId});
            
            modelBuilder.Entity<Game>()
                .HasOne(g=> g.DevCompany)
                .WithMany(c=> c.DevGames)
                .HasForeignKey(g=>g.DeveloperId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Game>()
                .HasOne(g=>g.Publisher)
                .WithMany(c=>c.PublishedGame)
                .HasForeignKey(g=>g.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        }

        
        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameCategory> GameCategories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LibraryGames> LibraryGames { get; set; }
    }
}