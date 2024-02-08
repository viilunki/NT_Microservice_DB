using Microsoft.EntityFrameworkCore;

namespace NT_Microservice_DB.Models
{
    [Keyless]
    public class ElectricityData
    {
        public decimal price { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
