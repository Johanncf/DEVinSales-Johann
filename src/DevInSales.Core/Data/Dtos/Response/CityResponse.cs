using DevInSales.Core.Entities;
namespace DevInSales.Core.Data.Dtos
{
    public class CityResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CityStateResponse State { get; set; }

        public static CityResponse? CityToReadCity(City? city)
        {
            if (city == null)
                return null;
            return new CityResponse
            {
                Id = city.Id,
                Name = city.Name,
                State = new CityStateResponse
                {
                    Id = city.State.Id,
                    Name = city.State.Name,
                    Initials = city.State.Initials
                }
            };
        }
    }
}
