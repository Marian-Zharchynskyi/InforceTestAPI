
using Domain.Urls;
using Domain.Users;
using Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UrlConfiguration : IEntityTypeConfiguration<Url>
{
    public void Configure(EntityTypeBuilder<Url> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new UrlId(x));

        builder.Property(x => x.OriginalUrl)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(x => x.ShortenedUrl)
            .HasMaxLength(1000);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .HasConversion(new DateTimeUtcConverter())
            .HasDefaultValueSql("timezone('utc', now())");
    }
}