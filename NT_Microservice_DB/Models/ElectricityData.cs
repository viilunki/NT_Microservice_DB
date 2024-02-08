using Microsoft.EntityFrameworkCore;

namespace NT_Microservice_DB.Models
{
    public class ElectricityData
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
