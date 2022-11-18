namespace MsSensediaTemplate.Application.ViewModels.Car.Requests
{
    public class PickupCarRequest
    {
        public Guid CarId { get; set; }
        public string RentedBy { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
    }
}
