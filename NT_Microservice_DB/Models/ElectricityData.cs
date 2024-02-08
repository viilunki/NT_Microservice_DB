using Microsoft.EntityFrameworkCore;

namespace NT_Microservice_DB.Models
{
    using Microsoft.EntityFrameworkCore;

    public class MyDbContext : DbContext
    {
        public DbSet<ElectricityData> ElectricityDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
    public class ElectricityData
    {
        public int Id { get; set; }
        public decimal price { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
