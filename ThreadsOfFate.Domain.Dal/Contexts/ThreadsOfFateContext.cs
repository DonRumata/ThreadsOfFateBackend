using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ThreadsOfFate.Domain.Dal.Contexts.Abstractions;
using ThreadsOfFate.Domain.Dal.Contexts.Extensions;
using ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell;

namespace ThreadsOfFate.Domain.Dal.Contexts
{
    class ThreadsOfFateContext : DbContextBase<ThreadsOfFateContext>, IThreadsOfFateContext
    {
        public ThreadsOfFateContext(DbContextOptions<ThreadsOfFateContext> options) : base(options)
        {

        }

        public  DbSet<Spell> Spells { get; set; }
        public DbSet<MagicElement> Elements { get; set; }
        public DbSet<MagicElementToSpell> MagicElementsToSpells { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Spell>().ToTable(nameof(Spell)).HasKey(sp => sp.Id);
            modelBuilder.Entity<MagicElement>().ToTable(nameof(MagicElement)).HasKey(sp => sp.Id);
            modelBuilder.Entity<MagicElementToSpell>().ToTable(nameof(MagicElementToSpell)).HasKey(ets =>
                new { SpellId = ets.SpellId, MagicElementId = ets.MagicElementId });

            modelBuilder.AddConfigurations();
        }
    }
}
