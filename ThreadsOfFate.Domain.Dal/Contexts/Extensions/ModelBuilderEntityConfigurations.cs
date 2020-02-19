using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ThreadsOfFate.Domain.Dal.Contexts.Extensions
{
    static class ModelBuilderEntityConfigurations
    {
        public static void AddConfigurations(this ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new SpellConfiguration());
        }
    }
}
