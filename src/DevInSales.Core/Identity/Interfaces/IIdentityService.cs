using DevInSales.Core.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Identity.Interfaces
{
    public interface IIdentityService
    {
        Task<RegistrationResponse> RegisterUserAsync(RegisterUserRequest userRequest);
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<ChangePasswordResponse> ChangePasswordAsync(string email, string currentPassword, string newPasswordt); 
        Task<SetRoleResponse> SetRoleAsync(SetRoleRequest roleRequest);  
    }
}
