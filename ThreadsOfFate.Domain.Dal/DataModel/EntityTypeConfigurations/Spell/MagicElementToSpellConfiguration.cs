using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell;

namespace ThreadsOfFate.Domain.Dal.DataModel.EntityTypeConfigurations.Spell
{
    class MagicElementToSpellConfiguration : IEntityTypeConfiguration<MagicElementToSpell>
    {
        public void Configure(EntityTypeBuilder<MagicElementToSpell> builder)
        {
            builder.ToTable("MagicElementToSpell", "dbo");
            builder.HasKey(e => new {SpellId = e.SpellId, MagicElementId = e.MagicElementId});

            builder.HasOne(e => e.MagicElementRef)
                .WithMany(e => e.MagicElementToSpellRef)
                .HasForeignKey(e => e.MagicElementId)
                .IsRequired(true);

            builder.HasOne(e => e.SpellRef)
                .WithMany(e => e.MagicElementToSpellRef)
                .HasForeignKey(e => e.SpellId)
                .IsRequired(true);
        }
    }
}
