using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell;

namespace ThreadsOfFate.Domain.Dal.DataModel.EntityTypeConfigurations.Spell
{
    class MagicElementConfiguration : IEntityTypeConfiguration<MagicElement>
    {
        public void Configure(EntityTypeBuilder<MagicElement> builder)
        {
            builder.ToTable("MagicElement", "dbo");
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.MagicElementToSpellRef)
                .WithOne(e => e.MagicElementRef);
        }
    }
}
