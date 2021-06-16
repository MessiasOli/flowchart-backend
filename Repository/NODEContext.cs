using Agricultural_Plan.Model;
using Microsoft.EntityFrameworkCore;

namespace Agricultural_Plan
{
    public class NODEContext : DbContext
    {
        /* Creating DatabaseContext without Dependency Injection */
        public DbSet<FlowchartJson> flowchartjson { get; set; }
        public DbSet<MatirialInput> materialinputs { get; set; }
        public DbSet<Area> areas { get; set; }

        public NODEContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlowchartJson>().HasKey(n => new { n.id });
            modelBuilder.Entity<MatirialInput>().HasKey(n => new { n.idarea, n.idmaterial });
            modelBuilder.Entity<Area>().HasKey(n => new { n.id });
        }
    }
}
