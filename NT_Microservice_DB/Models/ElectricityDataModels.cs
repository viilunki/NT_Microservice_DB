namespace NT_Microservice_DB.Models
{
    public class ElectricityDataRequestModel
    {
        public List<ElectricityDataItem> prices { get; set; }
    }

    public class ElectricityDataItem
    {
        public decimal price { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
