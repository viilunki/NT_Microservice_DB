using Microsoft.EntityFrameworkCore;

namespace NT_Microservice_DB.Models
{
    public class ElectricityData : BaseEntity
    {
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Price { get; set; }
    }
}
