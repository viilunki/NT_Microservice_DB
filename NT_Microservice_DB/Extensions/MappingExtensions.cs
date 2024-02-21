using NT_Microservice_DB.DTO;
using NT_Microservice_DB.Models;

namespace NT_Microservice_DB.Extensions
{
    public static class MappingExtensions
    {
        public static ElectricityData ToEntity(this PriceInfo priceInfo)
        {
            return new ElectricityData
            {
                StartDate = priceInfo.StartDate,
                EndDate = priceInfo.EndDate,
                Price = priceInfo.Price
            };
        }

    }
}
