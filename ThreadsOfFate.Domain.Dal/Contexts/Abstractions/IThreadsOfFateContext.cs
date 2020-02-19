using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ThreadsOfFate.Domain.Dal.Model.ThreadsOfFate.Implementation.Entity.Spell;

namespace ThreadsOfFate.Domain.Dal.Contexts.Abstractions
{
    interface IThreadsOfFateContext : IContext
    {
        DbSet<Spell> Spells { get; set; }
    }
}
