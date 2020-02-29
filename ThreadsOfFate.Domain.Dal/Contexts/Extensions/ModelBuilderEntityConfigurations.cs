using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ThreadsOfFate.Domain.Dal.DataModel.EntityTypeConfigurations.Spell;

namespace ThreadsOfFate.Domain.Dal.Contexts.Extensions
{
    static class ModelBuilderEntityConfigurations
    {
        public static void AddConfigurations(this ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new SpellConfiguration());
            //modelBuilder.ApplyConfiguration(new MagicElementConfiguration());
            modelBuilder.ApplyConfiguration(new MagicElementToSpellConfiguration());
        }
    }
}
