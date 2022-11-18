using MsSensediaTemplate.Domain.Models.Cars;

namespace MsSensediaTemplate.Domain.Interfaces.Repositories
{

    public interface ICarWriteOnlyRepository
    {
        Task Add(Cars car);
        Task Update(Cars car, PickUpCar pickUp);
        //Task Update(Car car, DropOffCar dropOff);
        Task Delete(Cars car);
    }
}
