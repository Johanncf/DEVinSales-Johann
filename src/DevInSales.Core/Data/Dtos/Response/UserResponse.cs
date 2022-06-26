using System.ComponentModel.DataAnnotations;
using DevInSales.Core.Entities;

namespace DevInSales.Core.Data.Dtos
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<RoleResponse> Roles { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public UserResponse(int id, string email, string name, DateTime birthDate, List<RoleResponse> roles)
        {
            Id = id;
            Email = email;
            Name = name;
            BirthDate = birthDate;
            Roles = roles;
        }

        //public static UserResponse ConverterParaEntidade(User user)
        //{
        //    return new UserResponse(user.Id, user.Email, user.Name, user.BirthDate);
        //}
    }
}