using MsSensediaTemplate.Application.Interfaces.Commands.RegisterCar;
using MsSensediaTemplate.Application.ViewModels.Car.Results;
using MsSensediaTemplate.Domain.Interfaces.Repositories;
using MsSensediaTemplate.Domain.Models.Cars;

namespace MsSensediaTemplate.Application.Commands.RegisterCar
{
    public sealed  class RegisterUseCase : IRegisterCarUseCase
    {


        private readonly ICarWriteOnlyRepository carWriteOnlyRepository;

        public RegisterUseCase(ICarWriteOnlyRepository carWriteOnlyRepository)
        {
            this.carWriteOnlyRepository = carWriteOnlyRepository;
        }

        public async Task<RegisterCarResult> Execute(string description, string plate)
        {
            Cars car = new Cars(description, plate);

            await carWriteOnlyRepository.Add(car);

            RegisterCarResult result = new RegisterCarResult(car);

            return result;
        }
    }
}
