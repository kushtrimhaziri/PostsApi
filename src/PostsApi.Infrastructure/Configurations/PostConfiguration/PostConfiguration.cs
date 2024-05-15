using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostsApi.Core.Domain;
using PostsApi.Infrastructure.Configurations.BaseConfiguration;


namespace PostsApi.Infrastructure.Configurations.PostConfiguration;



public class PostConfiguration : BaseEntityTypeConfiguration<Post>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Post> builder)
    {
        #region Properties
        
        builder.Property(c => c.Title).HasColumnName("Title").IsRequired();
        builder.Property(c => c.Content).HasColumnName("Content").IsRequired().HasMaxLength(1000);
        builder.Property(c => c.FriendlyUrl).HasColumnName("FriendlyUrl").IsRequired();
        
        #endregion

        #region Table

        builder.ToTable(name: "Posts");

        #endregion Table
    }
}
