using Microsoft.EntityFrameworkCore;
using SkillTest.Core.Domain.Entity;
using SkillTest.Core.Infrastructures.Data.TypeConfiguration;

namespace SkillTest.Core.Infrastructures.Data
{
    public class SkillTestDBContext : DbContext
    {
        public SkillTestDBContext(DbContextOptions<SkillTestDBContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
