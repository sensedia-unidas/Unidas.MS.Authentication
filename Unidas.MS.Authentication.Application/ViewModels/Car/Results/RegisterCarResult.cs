using MsSensediaTemplate.Domain.Models.Cars;

namespace MsSensediaTemplate.Application.ViewModels.Car.Results
{
    public class RegisterCarResult
    {
        public CarResult Car { get; }

        public RegisterCarResult(Cars car)
        {
            Car = new CarResult(car);

        }
    }
}
