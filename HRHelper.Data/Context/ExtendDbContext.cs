using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRHelper.Domain.Entities;
using HRHelper.Data.Mappings;

namespace HRHelper.Data.Context
{
    public class ExtendDbContext : DbContext
    {
        public ExtendDbContext(DbContextOptions<ExtendDbContext> options) : base(options) { }


        #region DBSets
        public DbSet<User> Users { get; set; } 
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
