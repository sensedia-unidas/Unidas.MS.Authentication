using MsSensediaTemplate.Domain.Models.Cars;

namespace MsSensediaTemplate.Domain.Interfaces.Repositories
{

    public interface ICarReadOnlyRepository
    {
        Task<Cars> Get(Guid id);
    }
}
