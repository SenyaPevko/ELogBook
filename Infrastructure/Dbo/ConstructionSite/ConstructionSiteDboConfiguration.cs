using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Dbo.ConstructionSite;

public class ConstructionSiteDboConfiguration : IEntityTypeConfiguration<ConstructionSiteDbo>
{
    public void Configure(EntityTypeBuilder<ConstructionSiteDbo> builder)
    {
        builder.HasIndex(s => s.Address).IsUnique();
        builder.HasIndex(s => s.Id).IsUnique();
    }
}