using DevInSales.Core.Entities;

namespace DevInSales.Core.Data.Dtos
{
    public class StateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Initials { get; set; }

        public static StateResponse? StateToReadState(State? state)
        {
            if (state == null)
                return null;
            return new StateResponse
            {
                Id = state.Id,
                Name = state.Name,
                Initials = state.Initials
            };
        }
    }
}
