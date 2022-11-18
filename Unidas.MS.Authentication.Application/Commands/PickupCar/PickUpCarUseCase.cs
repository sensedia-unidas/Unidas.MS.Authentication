using MsSensediaTemplate.Application.Interfaces.Commands.PickUpCar;
using MsSensediaTemplate.Application.Exceptions;
using MsSensediaTemplate.Domain.Interfaces.Repositories;
using MsSensediaTemplate.Domain.Models.Cars;
using MsSensediaTemplate.Application.Interfaces.Commands.PickUpCar;

namespace MsSensediaTemplate.Application.Commands.PickupCar
{    public sealed class PickUpCarUseCase : IPickUpCarUseCase
    {

        private readonly ICarReadOnlyRepository carReadOnlyRepository;
        private readonly ICarWriteOnlyRepository carWriteOnlyRepository;

        public PickUpCarUseCase(ICarReadOnlyRepository carReadOnlyRepository, ICarWriteOnlyRepository carWriteOnlyRepository)
        {
            this.carReadOnlyRepository = carReadOnlyRepository;
            this.carWriteOnlyRepository = carWriteOnlyRepository;
        }

        public async Task<Guid> Execute(Guid carId, string rentedBy, long latitude, long longitude)
        {
            Cars car = await carReadOnlyRepository.Get(carId);
            if (car == null)
                throw new CarNotFoundException($"O carro {carId} não existe");

           

            var pickUp = car.Pickup(rentedBy, latitude, longitude);

            await this.carWriteOnlyRepository.Update(car, pickUp);

            return pickUp.Id;
        }
    }
}
