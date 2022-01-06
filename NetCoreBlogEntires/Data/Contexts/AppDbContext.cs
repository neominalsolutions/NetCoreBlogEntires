using Microsoft.EntityFrameworkCore;
using NetCoreBlogEntires.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
            .HasMany(x => x.Comments)
            .WithOne()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {



            return base.SaveChanges();
        }


        // Commentlere direk ulşmak istemiyoruz zaten bir anlı yok bu sebeple PostRepository de Include ile çekmemiz lazım
        // İlgili postun taglerini ekranda göstermemiz gerekecek PostDetail (Blog Detay) sayfası için bu sebeple Post ile birlikte Tag Include yapmamız lazım.
        // İlgili Makalenin Kategorisiniş ekranda göstermemiz gerektiğinden Post içerisinde Category de Include lamalıyız.

    }
}
