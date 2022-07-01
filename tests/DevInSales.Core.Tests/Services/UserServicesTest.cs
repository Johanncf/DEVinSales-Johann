using DevInSales.Core.Data.Context;
using DevInSales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Tests.Services
{
    public class UserServicesTest
    {
        private readonly UserService _userService;
        
        public UserServicesTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "UserServiceTest")
                .Options;


            _userService = new UserService(new DataContext(options));
            for (int i = 1; i <= 10; i++)
            {
                _userService.CreateUser(new User($"teste{i}@service.com", $"nome{i}", new DateTime(1979 + i, i, i)));
            }
        }
        
        [Fact]
        public void GetUserById_ShouldReturn_User_WhenUserExists()
        {
            //Act
            var user = _userService.GetUserById(1);
            //Assert
            Assert.NotNull(user);
        }

        [Fact]
        public void GetUserById_ShouldThrow_ArgumentNullException_WhenUserDoesntExists()
        {
            Assert.Throws<ArgumentNullException>(() => _userService.GetUserById(0));
        }

        [Fact]
        public void GetUserById_Should_Return_User()
        {
            //Act
            var user = _userService.GetUserById(1);

            //Assert
            Assert.Equal("nome1", user.Name);
        }

        [Theory]
        [InlineData("nome0", null, null)]
        [InlineData(null, "1991-01-01", null)]
        [InlineData(null, null, "1970-01-01")]        
        public void GetUsers_ShouldReturn_EmptyList(string? nome, string? dataMin, string? dataMax)
        {
            //Act
            var users = _userService.GetUsers(nome, dataMin, dataMax);
            //Assert
            Assert.Empty(users);
        }

        [Theory]
        [InlineData("nome", null, null)]
        [InlineData(null, "1980-01-01", null)]
        [InlineData(null, null, "1990-01-01")]
        public void GetUsers_ShouldReturn_10ItemsList(string? nome, string? dataMin, string? dataMax)
        {
            //Act
            var users = _userService.GetUsers(nome, dataMin, dataMax);
            //Assert
            Assert.True(users.Count % 10 == 0);
        }

    }
}
