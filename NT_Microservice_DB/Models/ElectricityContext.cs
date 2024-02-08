using Microsoft.EntityFrameworkCore;

namespace NT_Microservice_DB.Models
{
    public class ElectricityContext : DbContext
    {
        public ElectricityContext(DbContextOptions<ElectricityContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ElectricityData>().HasNoKey();
        }


        public DbSet<ElectricityData> ElectricityDatas { get; set; } = null!;
    }
}
