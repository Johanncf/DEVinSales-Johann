using DevInSales.Core.Data.Context;
using DevInSales.Core.Data.Dtos;
using DevInSales.EFCoreApi.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevInSales.Core.Entities
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        public int CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }


        public UserResponse? GetUserById(int id)
        {
            var user = _context.Users
                .Include(user => user.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefault(user => user.Id == id);

            if (user is null) throw new ArgumentNullException();

            return new UserResponse(
                user.Id,
                user.Email,
                user.Name,
                user.BirthDate,
                user.UserRoles.Select(ur => new RoleResponse(ur.Role.Name, ur.RoleId)).ToList());
        }


        public List<UserResponse> GetUsers(string? name, string? DataMin, string? DataMax)
        {
            var query = _context.Users
                .Include(user => user.UserRoles)
                .ThenInclude(userRole => userRole.Role)
                .AsQueryable();
            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.Name.ToUpper().Contains(name.ToUpper()));
            if (!string.IsNullOrEmpty(DataMin))
                query = query.Where(p => p.BirthDate >= DateTime.Parse(DataMin));
            if (!string.IsNullOrEmpty(DataMax))
                query = query.Where(p => p.BirthDate <= DateTime.Parse(DataMax));

            var userList = query.ToList();

            var dtoList = query.Select(user => new UserResponse(
                    user.Id,
                    user.Email,
                    user.Name,
                    user.BirthDate,
                    user.UserRoles.Select(ur => new RoleResponse(ur.Role.Name, ur.Role.Id)).ToList()
                )
            ).ToList();

            return dtoList;
        }
        public void DeleteUser(int id)
        {
            if (id >= 0)
            {
                var user = _context.Users.FirstOrDefault(user => user.Id == id);
                if (user != null)
                    _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}