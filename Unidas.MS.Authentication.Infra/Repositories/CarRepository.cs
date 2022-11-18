using MsSensediaTemplate.Domain.Interfaces.Repositories;
using MsSensediaTemplate.Domain.Models.Cars;

namespace MsSensediaTemplate.Infra.Repositories
{
    public class CarRepository : ICarReadOnlyRepository, ICarWriteOnlyRepository
    {
        private readonly InMemoryDbContext _context;

        public CarRepository(InMemoryDbContext context)
        {
            _context = context;
        }
        public async Task Add(Cars car)
        {

            //Car carEntity = new Entities.Car()
            //{
            //   Description = car.Description,
            //   Id = car.Id,
            //   Plate = car.Plate
            //};

            _context.Cars.Add(car);
            await Task.CompletedTask;
        }

        public async Task Delete(Cars car)
        {
            Cars? carOld = _context.Cars.SingleOrDefault(e => e.Id == car.Id);

            if (carOld != null)
                _ = _context.Cars.Remove(carOld);

            await Task.CompletedTask;
        }

        public async Task<Cars> Get(Guid id)
        {
            Cars? carEntity = _context.Cars.SingleOrDefault(e => e.Id == id);

            List<ICarTransaction> carTransactions = new List<ICarTransaction>();

            List<PickUpCar> pickups = _context.Pickups.Where(x => x.CarId == id).ToList();

            foreach(PickUpCar transactionData in carTransactions)
            {
                PickUpCar pickup = PickUpCar.Load(transactionData.Id, transactionData.CarId, transactionData.RentedBy, transactionData.Latitude, transactionData.Longitude, transactionData.TransactionDate);

                carTransactions.Add(pickup);
            }


            var orderedTransactions = carTransactions.OrderBy(o => o.TransactionDate).ToList();

            CarTransactionCollection transactionCollection = new CarTransactionCollection();
            transactionCollection.Add(orderedTransactions);

            Cars car = Cars.Load(carEntity.Id, carEntity.Description, carEntity.Plate, transactionCollection);

            return await Task.FromResult<Cars>(car);
        }

        public async Task Update(Cars car, PickUpCar pickUp)
        {
            PickUpCar pickupEntity = PickUpCar.Load(pickUp.Id, car.Id, pickUp.RentedBy, pickUp.Latitude, pickUp.Longitude,  pickUp.TransactionDate);

            _context.Pickups.Add(pickupEntity);

            await Task.CompletedTask;

        }
    }
}
