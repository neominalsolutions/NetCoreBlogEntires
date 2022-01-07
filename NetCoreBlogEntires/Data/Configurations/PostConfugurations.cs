using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreBlogEntires.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreBlogEntires.Data.Configurations
{
    // FLUENT API yöntemi diyoruz.
    // onModel Creating içerisine bu configurasyonu ekleriz.
    public class PostConfugurations : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Title).IsUnique(); // DB tarafında aynı isim verilemez.
            builder.Property(x => x.Title).HasColumnName("Makale Başlıgı");
            builder.ToTable("Makale");

            // 1 to n relation
            builder
           .HasMany(x => x.Comments)
           .WithOne()
           .IsRequired()
           .OnDelete(DeleteBehavior.Cascade);

            // n to n relation
            builder
          .HasMany(x => x.Tags)
          .WithMany(x => x.Posts);

            builder.Property(x => x.ShortContent).HasMaxLength(500).HasColumnType("nvarchar");

            // 1 postun birden fazla commenti var yani ilişkilerin yönetiminde yapıyoruz. ilişki yönetimleri aggregate root tan yapılır.


        }
    }
}
