using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel;

namespace Infrastructure.EF.Contexts.AppConfig;
public abstract class EntityConfig<TAggregateRoot> where TAggregateRoot : Entity
{
    public virtual void Configure(EntityTypeBuilder<TAggregateRoot> builder)
    {
        builder.Property((TAggregateRoot e) => e.Id).ValueGeneratedNever().IsRequired();
        builder.HasKey((TAggregateRoot e) => e.Id);
        builder.Ignore((TAggregateRoot e) => e.DomainEvents);
    }
}
