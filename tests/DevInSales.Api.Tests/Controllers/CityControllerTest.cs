using DevInSales.Api.Controllers;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Api.Tests.Controllers
{
    public class CityControllerTest
    {
        private CityController _controller;
        private Mock<ICityService> _mockCityService;
        private Mock<IStateService> _mockStateService;
        public CityControllerTest()
        {
            _mockCityService = new Mock<ICityService>();
            _mockStateService = new Mock<IStateService>();
            _controller = new CityController(
                _mockStateService.Object, _mockCityService.Object);
        }

        [Fact]
        public void GetCitiesByStateId_ShouldReturn_NotFound404()
        {
            // Arrange
            _mockStateService.Setup(x => x.GetById(It.IsAny<int>())).Returns(() => null);
            // Act
            var result = _controller.GetCitiesByStateId(1, "teste");
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetCitiesByStateId_ShouldReturn_NoContent204()
        {
            // Arrange
            int stateId = 1;
            var stateResponseMock = new StateResponse()
            {
                Id = stateId,
                Name = "teste",
                Initials = "RJ"
            };
            _mockStateService.Setup(x => x.GetById(stateId)).Returns(stateResponseMock);
            _mockCityService.Setup(x => x.GetAll(stateId, "teste")).Returns(() => null);
            // Act
            var result = _controller.GetCitiesByStateId(stateId, "teste");
            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetCitiesByStateId_ShouldReturn_Ok200()
        {
            // Arrange
            int stateId = 1;
            var stateResponseMock = new StateResponse()
            {
                Id = stateId,
                Name = "teste",
                Initials = "RJ"
            };
            _mockStateService.Setup(x => x.GetById(stateId)).Returns(stateResponseMock);
            _mockCityService.Setup(x => x.GetAll(stateId, "teste")).Returns(
                new List<CityResponse>()
                {
                    new CityResponse() 
                    { 
                        Id = 1, 
                        Name = "Teste", 
                        State = new CityStateResponse()
                        {
                            Id = 1,
                            Name = "Teste",
                            Initials = "RJ"
                        }
                    }
                });
            // Act
            var result = _controller.GetCitiesByStateId(stateId, "teste");
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public void GetCitiesById_ShouldReturn_NotFound404(int stateId, int cityId)
        {
            // Arrange
            _mockStateService.Setup(x => x.GetById(1)).Returns(() => null);
            _mockCityService.Setup(x => x.GetById(1)).Returns(() => null);
            // Act
            var result = _controller.GetCitiesByStateId(stateId, "teste");
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetCitiesById_ShouldReturn_BadRequest400()
        {
            // Arrange
            int stateId = 1;
            int cityId = 1;
            _mockStateService.Setup(x => x.GetById(stateId)).Returns(
                new StateResponse()
                {
                    Id = stateId,
                    Name = "Outro",
                    Initials = "SP"
                });
            _mockCityService.Setup(x => x.GetById(1)).Returns(
                new CityResponse()
                {
                    Id = cityId,
                    Name = "Teste",
                    State = new CityStateResponse()
                    {
                        Id = 2,
                        Name = "Teste",
                        Initials = "RJ"
                    }
                });
            // Act
            var result = _controller.GetCityById(stateId, cityId);
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
