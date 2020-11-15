using LinkMe.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinkMe.Data.Configuration
{
    public class LinkConfiguration : IEntityTypeConfiguration<Link>
    {
        public void Configure(EntityTypeBuilder<Link> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.OriginalLink).HasMaxLength(100);

            builder.Property(e => e.ShortLink).HasMaxLength(15);

            builder.Property(e => e.ValidTo).HasColumnType("date");
        }
    }
}
