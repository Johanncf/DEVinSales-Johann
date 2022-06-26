using Microsoft.AspNetCore.Identity;

namespace DevInSales.Core.Entities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; private set; }
        public DateTime BirthDate { get; private set; }
        public List<UserRole> UserRoles { get; set; }

        public User(string email, string name, DateTime birthDate)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            UserRoles = new List<UserRole>();
        }
        public User(int id, string email, string name, DateTime birthDate)
        {
            Id = id;
            Email = email;
            Name = name;
            BirthDate = birthDate;
            UserRoles = new List<UserRole>();
        }   
    }
}