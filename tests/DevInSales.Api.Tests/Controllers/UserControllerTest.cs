using DevInSales.Api.Controllers;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Identity.Interfaces;
using DevInSales.EFCoreApi.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Api.Tests.Controllers
{
    public class UserControllerTest
    {
        private Mock<IUserService> _userServiceMock;
        private Mock<IIdentityService> _identityServiceMock;
        private UserController _userController;
        public UserControllerTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _identityServiceMock = new Mock<IIdentityService>();
            _userController = new UserController(
                _userServiceMock.Object,
                _identityServiceMock.Object
            );
        }

        [Theory]
        [InlineData(1)]
        public void GetUserByd_Should_Return_200OkObjectResultUserResponse(int id)
        {
            //Arrange
            int idMock = 1;
            var userMock = new UserResponse(idMock, "teste@mock.com", "Mock", DateTime.Now, new List<RoleResponse>() { new RoleResponse("user", 3) });
            _userServiceMock.Setup(s => s.GetUserById(idMock)).Returns(userMock);
            
            //Act
            var result = _userController.GetUserById(id);
            
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void GetUserByd_Should_Return_404NotFoundObjectResult(int id)
        {
            //Arrange
            int idMock = 1;
            UserResponse userMock = null;
            _userServiceMock.Setup(s => s.GetUserById(idMock)).Returns(userMock);

            //Act
            var result = _userController.GetUserById(id);
            
            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task RegisterUser_Should_Return_403ForbidResult()
        {
            //Arrange
            RegisterUserRequest requestMock = new RegisterUserRequest()
            {
                Name = "Mock",
                BirthDate = DateTime.Now,
                Email = "teste@mock.com",
                Password = "123456",
                PasswordConfirmation = "123456"
            };

            //Act
            var result = await _userController.RegisterUser(requestMock);

            //Assert
            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task RegisterUser_Should_Return_400BadRequestObjectResult()
        {
            //Arrange
            RegisterUserRequest requestMock = new RegisterUserRequest()
            {
                Name = "Mock",
                BirthDate = new DateTime(1980, 1, 1),
                Email = "teste@mock.com",
                Password = "Teste123*",
                PasswordConfirmation = "Teste123*"
            };

            RegistrationResponse responseMock = new RegistrationResponse();
            responseMock.AddErrors(new List<string>() { "erro1", "erro2", "erro3" });

            _identityServiceMock.Setup(m => m.RegisterUserAsync(requestMock))
                .ReturnsAsync(responseMock);

            //Act
            var result = await _userController.RegisterUser(requestMock);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task RegisterUser_Should_Return_200OkObjectResultUserResponse()
        {
            //Arrange
            RegisterUserRequest requestMock = new RegisterUserRequest()
            {
                Name = "Mock",
                BirthDate = new DateTime(1980, 1, 1),
                Email = "teste@mock.com",
                Password = "Teste123*",
                PasswordConfirmation = "Teste123*"
            };

            RegistrationResponse responseMock = new RegistrationResponse();

            _identityServiceMock.Setup(m => m.RegisterUserAsync(requestMock))
                .ReturnsAsync(responseMock);

            //Act
            var result = await _userController.RegisterUser(requestMock);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
