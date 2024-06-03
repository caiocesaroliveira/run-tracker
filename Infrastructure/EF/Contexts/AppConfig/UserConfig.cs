using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EF.Contexts.AppConfig;
internal class UserConfig : EntityConfig<User>, IEntityTypeConfiguration<User>
{
    /// <inheritdoc cref="IEntityTypeConfiguration{TEntity}.Configure(EntityTypeBuilder{TEntity})"/>
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.OwnsOne(x => x.Name, b =>
        {
            b.Property<string>("Valor")
             .HasMaxLength(200)
             .IsRequired();


        });

        builder.OwnsOne(x => x.Email, b =>
        {
            b.Property<string>("Valor")
             .HasMaxLength(512)
             .IsRequired();

            b.HasIndex("Valor")
            .IsUnique();
        });

        builder
            .Property(x => x.HasPublicProfile)
            .IsRequired();
    }
}
