namespace MsSensediaTemplate.Application.Interfaces.Commands.PickUpCar
{

    using System.Threading.Tasks;

    public interface IPickUpCarUseCase
    {
        Task<Guid> Execute(Guid carId, string rentedBy, long latitude, long longitude);
    }
}
