using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace ProjectInfrastructure.Database;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Tasks)
            .HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions)null));
        builder.Property(x => x.Users)
            .HasConversion(v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null), v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions)null));
    }
}
