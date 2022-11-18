namespace MsSensediaTemplate.Domain.Models.Cars
{
    public sealed class Cars : IEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }

        public string Description { get; set; }

       

        public string Plate { get; set; }


        public IReadOnlyCollection<ICarTransaction> GetTransactions()
        {
            IReadOnlyCollection<ICarTransaction> readOnly = _transactions.GetTransactions();
            return readOnly;
        }

        private CarTransactionCollection _transactions;

        public Cars(string description, string plate)
        {
            Id = Guid.NewGuid();
            Description = description;
            Plate = plate;
            _transactions = new CarTransactionCollection();
        }

        public PickUpCar Pickup(string rentedBy, long latitude, long longitude)
        {
            if (_transactions.GetLastTransactionType() == "PickUp")
                throw new CarCannotBePickupExcepction($"O carro {Id} já está alugado!");


            var pickUp = new PickUpCar(Id, rentedBy, latitude, longitude);

            _transactions.Add(pickUp);

            return pickUp;
        }

        public (long latitude, long longitude) GetLastPosition()
        {
            var lastTransaction = _transactions.GetLastTransaction();

            if (lastTransaction == null)
                throw new CarWithoutTransactionException($"O carro {Id} não possúi transação");

            return (lastTransaction.Latitude, lastTransaction.Longitude);
        }

        private Cars() { }

        public static Cars Load(Guid id, string description, string Plate, CarTransactionCollection transactions)
        {
            Cars car = new Cars();
            car.Id = id;
            car.Description = description;
            car.Plate = Plate;
            car._transactions = transactions;
            return car;

        }
    }
}
