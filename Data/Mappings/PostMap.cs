using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings;

public class PostMap : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        // Tabela
        builder.ToTable("Post");

        // Chave
        builder.HasKey(x => x.Id);

        // Identity
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        // Campos
        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.Summary)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.Body)
            .IsRequired()
            .HasColumnType("NVARCHAR(MAX)");

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(160);

        builder.Property(x => x.CreateDate)
            .IsRequired();

        builder.Property(x => x.LastUpdateDate)
            .IsRequired()
            .HasColumnName("LastUpdateDate")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValue(DateTime.Now.ToUniversalTime());

        // Índices
        builder.HasIndex(x => x.Slug, "IX_Post_Slug")
            .IsUnique();

        // Relações
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Posts)
            .HasForeignKey("CategoryId")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(x => x.Author)
            .WithMany(x => x.Posts)
            .HasForeignKey("AuthorId")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Tags)
            .WithMany(x => x.Posts)
            .UsingEntity<Dictionary<string, object>>(
                "PostTag",
                right => right.HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("TagId")
                    .OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<Post>()
                    .WithMany()
                    .HasForeignKey("PostId")
                    .OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.HasKey("PostId", "TagId");
                    join.ToTable("PostTag");
                });
    }
}