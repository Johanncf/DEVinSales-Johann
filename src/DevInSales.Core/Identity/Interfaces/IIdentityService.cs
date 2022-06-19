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
        Task<RegistrationResponse> RegisterUser(RegisterUserRequest userRequest);
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
