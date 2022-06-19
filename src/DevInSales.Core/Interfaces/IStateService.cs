using DevInSales.Core.Data.Dtos;

namespace DevInSales.Core.Interfaces
{
    public interface IStateService
    {
        List<StateResponse> GetAll(string? name);
        StateResponse GetById(int stateId);
    }
}