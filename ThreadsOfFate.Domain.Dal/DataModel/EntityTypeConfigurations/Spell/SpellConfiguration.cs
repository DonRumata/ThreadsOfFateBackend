using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell;

namespace ThreadsOfFate.Domain.Dal.DataModel.EntityTypeConfigurations.Spell
{
    class SpellConfiguration : IEntityTypeConfiguration<Model.ThreadsOfFate.Implementation.Entity.Spell.Spell>
    {
        public void Configure(EntityTypeBuilder<Model.ThreadsOfFate.Implementation.Entity.Spell.Spell> builder)
        {
            builder.ToTable("Spell", "dbo");
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.MagicElementToSpellRef).WithOne(e => e.SpellRef);
        }
    }
}
