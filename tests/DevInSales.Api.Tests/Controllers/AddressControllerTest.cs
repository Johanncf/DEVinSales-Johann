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
    public class AddressControllerTest
    {
        private Mock<IAddressService> _addressServiceMock;
        private Mock<ICityService> _cityServiceMock;
        private Mock<IStateService> _stateServiceMock;
        private AddressController _addressController;

        public AddressControllerTest()
        {
            _addressServiceMock = new Mock<IAddressService>();
            _cityServiceMock = new Mock<ICityService>();
            _stateServiceMock = new Mock<IStateService>();
            _addressController = new AddressController(
                _addressServiceMock.Object,
                _stateServiceMock.Object,
                _cityServiceMock.Object
            );
        }

        [Theory]
        [InlineData(1, 1)]
        public void AddAddress_ShouldReturn_404NotFoundResult(int stateId, int cityId)
        {
            //Arrange
            _stateServiceMock.Setup(s => s.GetById(stateId)).Returns<StateResponse>(null);
            _cityServiceMock.Setup(c => c.GetById(cityId)).Returns<CityResponse>(null);
            var addAddressRequestMock = new AddAddressRequest()
            {
                Street = "Rua Teste",
                Number = 123,
                Cep = "12345-678"
            };
            
            //Act
            var result = _addressController.AddAddress(stateId, cityId, addAddressRequestMock);

            //Assert
            Assert.IsType<NotFoundResult>(result);    
        }

        [Theory]
        [InlineData(1, 1)]
        public void AddAddress_ShouldReturn_400BadRequestResult(int stateId, int cityId)
        {
            //Arrange
            var cityResponseMock = new CityResponse()
            {
                Id = cityId,
                Name = "Teste",
                State = new CityStateResponse()
                {
                    Id = 10,
                    Name = "Teste"
                }
            };

            var stateResponseMock = new StateResponse()
            {
                Id = stateId,
                Name = "Teste",
                Initials = "TT"
            };

            _cityServiceMock.Setup(c => c.GetById(cityId)).Returns(cityResponseMock);
            _stateServiceMock.Setup(s => s.GetById(stateId)).Returns(stateResponseMock);

            var addAddressRequestMock = new AddAddressRequest()
            {
                Street = "Rua Teste",
                Number = 123,
                Cep = "12345-678"
            };

            //Act
            var result = _addressController.AddAddress(stateId, cityId, addAddressRequestMock);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
