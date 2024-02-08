using Microsoft.EntityFrameworkCore;

namespace NT_Microservice_DB.Models
{
    public class ElectricityContext : DbContext
    {
        public DbSet<ElectricityData> ElectricityData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
