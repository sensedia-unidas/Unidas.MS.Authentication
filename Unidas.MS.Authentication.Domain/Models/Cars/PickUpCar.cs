namespace MsSensediaTemplate.Domain.Models.Cars
{
    public sealed class PickUpCar : IEntity, ICarTransaction
    {
       

        public Guid Id { get; private set; }
        public Guid CarId { get; private set; }

        public DateTime TransactionDate { get; private set; }

        public string RentedBy { get; set; }       

        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public string Action
        {
            get { return "PickUp"; }
        }

        private PickUpCar() { }

        public static PickUpCar Load(Guid id, Guid carId, string rentedBy, long latitude, long longitude, DateTime transactionDate)
        {
            PickUpCar pickup = new PickUpCar();
            pickup.Id = id;
            pickup.CarId = carId;
            pickup.RentedBy =rentedBy;
            pickup.Latitude = latitude;
            pickup.Longitude = longitude;
            pickup.TransactionDate = transactionDate;
            return pickup;
        }

        public PickUpCar(Guid carId, string rentedBy, long latitude, long longitude)
        {
            Id = Guid.NewGuid();
            CarId = carId;
            RentedBy = rentedBy;
            Latitude = latitude;
            Longitude = longitude;
            TransactionDate = DateTime.UtcNow;
        }

    }
}
