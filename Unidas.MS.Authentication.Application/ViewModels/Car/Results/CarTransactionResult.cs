namespace MsSensediaTemplate.Application.ViewModels.Car.Results
{
    public sealed class CarTransactionResult
    {
        public string Action { get; set; }
        public string RentedBy { get; set; }
        public DateTime TransactionDate { get; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public CarTransactionResult(string action, string rentedBy, DateTime transactionDate, long latitude, long longitude)
        {
            Action = action;
            RentedBy = rentedBy;
            TransactionDate = transactionDate;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
