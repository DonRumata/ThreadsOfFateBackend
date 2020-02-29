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
            //modelBuilder.Entity<Spell>()
            //    .HasMany(sp => sp.SpellToMagicElementToSpells)
            //    .WithOne(sp => sp.SpellRef);

            modelBuilder.Entity<Spell>().ToTable(nameof(Spell)).HasKey(t => t.Id);

            //modelBuilder.Entity<MagicElement>()
            //    .HasMany(sp => sp.MagicElementToMagicElementToSpells)
            //    .WithOne(sp => sp.MagicElementRef);
            modelBuilder.Entity<MagicElement>().ToTable(nameof(MagicElement));

            modelBuilder.Entity<MagicElementToSpell>()
                .ToTable(nameof(MagicElementToSpell));

            //modelBuilder.Entity<MagicElementToSpell>().HasOne(sp => sp.SpellRef)
            //    .WithMany(sp => sp.MagicElementToSpells);
            //    //.HasForeignKey(sp => sp.SpellId);
            //modelBuilder.Entity<MagicElementToSpell>().HasOne(el => el.MagicElementRef)
            //    .WithMany(el => el.MagicElementToSpells);
            //    //.HasForeignKey(el => el.MagicElementId);

            modelBuilder.Entity<MagicElementToSpell>().HasKey(ets => new { SpellId = ets.SpellId, MagicElementId = ets.MagicElementId });
            //magicElementToSpellBuilder.HasOne(t => t.SpellRef)
            //    .WithMany(t => t.MagicElementToSpells)
            //    .HasForeignKey(t => t.SpellId);
            //magicElementToSpellBuilder.HasOne(t => t.MagicElementRef)
            //    .WithMany(t => t.MagicElementToSpells)
            //    .HasForeignKey(t => t.MagicElementId);

            modelBuilder.AddConfigurations();
        }
    }
}
