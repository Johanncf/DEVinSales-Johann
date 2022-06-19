using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;

namespace DevInSales.Core.Interfaces
{
    public interface ICityService
    {
        List<CityResponse> GetAll(int stateId, string? name);
        CityResponse GetById(int cityId);

        void Add(City city);
    }
}