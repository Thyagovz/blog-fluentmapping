using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings;

public class TagMap : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        // Tabela
        builder.ToTable("Tag");

        // Chave
        builder.HasKey(x => x.Id);

        // Identity
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        // Campos
        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Slug)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(80);

        // Índices
        builder.HasIndex(x => x.Slug, "IX_Tag_Slug")
            .IsUnique();
    }
}
