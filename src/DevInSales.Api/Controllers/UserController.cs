using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Identity.Constants;
using DevInSales.Core.Identity.Interfaces;
using DevInSales.EFCoreApi.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegexExamples;

namespace DevInSales.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IIdentityService _identityService;

        public UserController(IUserService userService, IIdentityService identityService)
        {
            _userService = userService;
            _identityService = identityService;
        }



        /// <summary>
        /// Busca uma lista de usuários.
        /// </summary>
        /// <remarks>
        /// Pesquisa opcional: nome,data minima, data máxima
        /// <para>
        /// Exemplo de resposta:
        /// [
        ///   {
        ///     "id": 1,
        ///     "email": "joao@hotmail.com",
        ///     "name": "João",
        ///     "birthDate": "01/01/2000"
        ///   }
        /// ]
        /// </para>
        /// </remarks>
        /// <returns>Lista de endereços</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Pesquisa realizada com sucesso porém não retornou nenhum resultado</response>

        [Authorize(Roles = Roles.Admin)]
        [Authorize(Roles = Roles.Gerente)]
        [HttpGet]
        public ActionResult<List<UserResponse>> ObterUsers(string? nome, string? DataMin, string? DataMax)
        {

            var users = _userService.ObterUsers(nome, DataMin, DataMax);
            if (users == null || users.Count == 0)
                return NoContent();


            return Ok(users);
        }



        /// <summary>
        /// Busca um usuário por id.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Exemplo de resposta:
        /// [
        ///   {
        ///     "id": 1,
        ///     "email": "joao@hotmail.com",
        ///     "name": "João",
        ///     "birthDate": "01/01/2000"
        ///   }
        /// ]
        /// </para>
        /// </remarks>
        /// <returns>Lista de endereços</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="404">Not Found, estado não encontrado no stateId informado.</response>
        [HttpGet("{id}")]
        public ActionResult<User> ObterUserPorId(int id)
        {
            var userDTO = _userService.ObterPorId(id);
            if (userDTO == null)
                return NotFound();

            return Ok(userDTO);
        }

        /// <summary>
        /// Cadastra um novo usuário.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Exemplo de resposta:
        /// [
        ///   {
        ///     "id": 1
        ///   }
        /// ]
        /// </para>
        /// </remarks>
        /// <returns>Lista de endereços</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Pesquisa realizada com sucesso porém não retornou nenhum resultado</response>
        /// <response code="400">Formato invalido</response>
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(RegisterUserRequest model)
        {
            if (model.BirthDate.AddYears(18) > DateTime.Now)
                return Forbid();


            var response = await _identityService.RegisterUserAsync(model);
            if (!response.Success) return BadRequest(response);

            return Ok(response);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var response = await _identityService.LoginAsync(request);
                if (response is null) return StatusCode(StatusCodes.Status500InternalServerError, "Not tracked server error.");
                if (!response.Success) return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                var response = await _identityService.ChangePasswordAsync(request.Email, request.CurrentPassword, request.NewPassword);

                if (!response.Success) return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            
        }

        /// <summary>
        /// Deleta um usuário.
        /// </summary>
        /// <response code="204">Endereço deletado com sucesso</response>
        /// <response code="404">Not Found, endereço não encontrado.</response>
        /// <response code="500">Internal Server Error, erro interno do servidor.</response>
        [Authorize(Roles = Roles.Admin)]
        [Authorize(Roles = Roles.Gerente)]
        [HttpDelete("{id}")]
        public ActionResult ExcluirUser(int id)
        {
            try
            {
                _userService.RemoverUser(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("usuario não existe"))
                    return NotFound();

                return StatusCode(StatusCodes.Status500InternalServerError, new { Mensagem = ex.Message });
            }
        }
    }
}