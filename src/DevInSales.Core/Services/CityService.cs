using DevInSales.Core.Data.Context;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Core.Services
{
    public class CityService : ICityService
    {
        private readonly DataContext _context;

        public CityService(DataContext context)
        {
            _context = context;
        }

        public List<CityResponse> GetAll(int stateId, string? name)
        {
            return _context.Cities
                .Where(
                    p =>
                        p.StateId == stateId
                        && (
                            !String.IsNullOrEmpty(name)
                                ? p.Name.ToUpper().Contains(name.ToUpper())
                                : true
                        )
                )
                .Select(c => CityResponse.CityToReadCity(c))
                .ToList();
        }

        public CityResponse GetById(int cityId)
        {
            var city = _context.Cities
                .Include(p => p.State)
                .FirstOrDefault(p => p.Id == cityId);

            return CityResponse.CityToReadCity(city);
        }

        public void Add(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
        }
    }
}
