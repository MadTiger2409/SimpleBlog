using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data.Models;

namespace SimpleBlog.Data.Database
{
    public class BlogContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Message> Messages { get; set; }

        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                    .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<User>()
                .HasMany(x => x.Comments)
                .WithOne(y => y.User)
                .IsRequired(false);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Comments)
                .WithOne(y => y.Post)
                .IsRequired(false);

            modelBuilder.Entity<Admin>()
                .HasMany(x => x.Posts)
                .WithOne(y => y.Admin)
                .IsRequired(false);

            modelBuilder.Entity<Admin>()
                .HasData(new Admin("Adm1n.P@ss", "SuperAdmin666") { Id = 1});

            modelBuilder.Entity<Message>()
                .HasKey(x => x.Id);
        }
    }
}
