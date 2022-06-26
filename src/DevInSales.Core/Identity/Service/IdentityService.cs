using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Identity.Configuration;
using DevInSales.Core.Identity.Constants;
using DevInSales.Core.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Identity.Service
{
    public class IdentityService : IIdentityService
    {
        private readonly SignInManager<User> _signinManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(SignInManager<User> signinManager, UserManager<User> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _signinManager = signinManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var result = await _signinManager.PasswordSignInAsync(
                loginRequest.Email,
                loginRequest.Password,
                false,
                false);

            var loginResponse = new LoginResponse();

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                    loginResponse.AddError("A conta está bloqueada");
                else if (result.IsNotAllowed)
                    loginResponse.AddError("Este usuário não tem permissão para acessar este recurso");
                else if (result.RequiresTwoFactor)
                    loginResponse.AddError("É necessário segundo fator de autenticação.");

                loginResponse.AddError("Erro genérico");
                
                return loginResponse;
            }

            var jwt = await GenerateTokenAsyn(loginRequest.Email);
            loginResponse.SetToken(jwt);
            return loginResponse;
        }

        public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserRequest userRequest)
        {
            var user = new User(userRequest.Email, userRequest.Name, userRequest.BirthDate)
            {
                UserName = userRequest.Email
            };

            var result = await _userManager.CreateAsync(user, userRequest.Password);

            var registrationResponse = new RegistrationResponse();

            if (!result.Succeeded && result.Errors.Count() > 0)
            {
                registrationResponse.AddErrors(result.Errors.Select(error => 
                    error.Description).ToList());

                return registrationResponse;
            }
            await _userManager.AddToRoleAsync(user, Roles.Usuario);
            await _userManager.SetLockoutEnabledAsync(user, false);
            return registrationResponse;
        }

        public async Task<ChangePasswordResponse> ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            var response = new ChangePasswordResponse();

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    response.AddError(error.Description);   
                }
            }

            return response;
        }

        public async Task<SetRoleResponse> SetRoleAsync(SetRoleRequest setRoleRequest)
        {
            var user = await _userManager.FindByIdAsync(setRoleRequest.UserId.ToString());

            var result = await _userManager.AddToRoleAsync(user, setRoleRequest.RoleName);   

            SetRoleResponse response = new SetRoleResponse();
            if (!result.Succeeded && !result.Errors.Any())
            {
                IEnumerable<string> errors = result.Errors.Select(error => error.Description.ToString());
                response.AddErrors(errors);
            }

            return response;
        }

        private async Task<string> GenerateTokenAsyn(string email)
        {
            var user =  await _userManager.FindByEmailAsync(email);
            var tokenClaims = await GetClaims(user);
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(tokenClaims);
            var expirationDate = DateTime.Now.AddDays(_jwtOptions.Expiration);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                notBefore: DateTime.Now,
                expires: expirationDate,
                signingCredentials: _jwtOptions.SigningCredentials,
                claims: identityClaims.Claims);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<IEnumerable<Claim>> GetClaims(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            return roles.Select(role => new Claim(ClaimTypes.Role, role));
        }
    }
}
