using Microsoft.EntityFrameworkCore;

namespace NT_Microservice_DB.Models
{
    public class ElectricityContext : DbContext
    {
        public DbSet<ElectricityData> ElectricityDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Encrypt=True;Trust Server Certificate=False;Command Timeout=0");
        }
    }
}
