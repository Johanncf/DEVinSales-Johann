using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;

namespace DevInSales.EFCoreApi.Core.Interfaces
{
    public interface IUserService
    {
        public List<UserResponse> ObterUsers(string? name, string? DateMin, string? DateMax);

        public UserResponse? ObterPorId(int id);

        public int CriarUser(User user);

        public void RemoverUser(int id);
    }
}