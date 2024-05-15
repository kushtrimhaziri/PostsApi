using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostsApi.Core.Domain;

namespace PostsApi.Infrastructure.Configurations.BaseConfiguration;

public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasColumnName("CreatedBy").IsRequired(false);
        
        builder.Property(x => x.CreatedOn)
            .HasColumnName("CreatedOn")
            .IsRequired();

        builder.Property(x => x.UpdatedBy)
            .HasColumnName("UpdatedBy").IsRequired(false);
        
        builder.Property(x => x.UpdatedOn)
            .HasColumnName("UpdatedOn")
            .IsRequired();

        builder.Property(x => x.Deleted)
            .HasColumnName("Deleted")
            .HasDefaultValue(false)
            .ValueGeneratedOnAdd()
            .IsRequired();

        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}
