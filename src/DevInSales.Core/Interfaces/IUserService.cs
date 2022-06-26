using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;

namespace DevInSales.EFCoreApi.Core.Interfaces
{
    public interface IUserService
    {
        public List<UserResponse> GetUsers(string? name, string? DateMin, string? DateMax);

        public UserResponse? GetUserById(int id);

        public int CreateUser(User user);

        public void DeleteUser(int id);
    }
}