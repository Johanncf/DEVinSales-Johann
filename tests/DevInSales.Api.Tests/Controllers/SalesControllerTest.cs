using DevInSales.Api.Controllers;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
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
    public class SalesControllerTest
    {
        private Mock<ISaleService> _salesServiceMock;
        private SalesController _salesController;

        public SalesControllerTest()
        {
            _salesServiceMock = new Mock<ISaleService>();
            _salesController = new SalesController(_salesServiceMock.Object);
        }

        [Fact]
        public void GetSaleById_ShouldReturn_200Ok()
        {
            //Arrange
            var salesId = 1;
            var saleDateMock = new DateTime(1980, 1, 1);
            var saleProductsMock = new List<SaleProductResponse>()
            {
                new SaleProductResponse("Teste", 1, 10),
                new SaleProductResponse("Teste", 2, 10),
                new SaleProductResponse("Teste", 3, 10)
            };
            var saleResponseMock = new SaleResponse(1, "SellerTeste", "BuyerTeste", saleDateMock, saleProductsMock);

            _salesServiceMock.Setup(x => x.GetSaleById(salesId)).Returns(saleResponseMock);

            //Act
            var result = _salesController.GetSaleById(salesId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetSaleById_ShouldReturn_404NotFound()
        {
            //Arrange
            var salesId = 1;
            var saleDateMock = new DateTime(1980, 1, 1);
            var saleProductsMock = new List<SaleProductResponse>()
            {
                new SaleProductResponse("Teste", 1, 10),
                new SaleProductResponse("Teste", 2, 10),
                new SaleProductResponse("Teste", 3, 10)
            };
            var saleResponseMock = new SaleResponse(1, "SellerTeste", "BuyerTeste", saleDateMock, saleProductsMock);

            _salesServiceMock.Setup(x => x.GetSaleById(salesId)).Returns<Sale>(null);

            //Act
            var result = _salesController.GetSaleById(salesId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetSalesBySellerId_ShouldReturn_204NoContent()
        {
            //Arrange 
            int sellerId = 1;
            var salesListMock = new List<Sale>() { };

            _salesServiceMock.Setup(x => x.GetSaleBySellerId(sellerId)).Returns(salesListMock);

            //Act
            var result = _salesController.GetSalesBySellerId(sellerId);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetSalesBySellerId_ShouldReturn_200Ok()
        {
            //Arrange
            int sellerId = 1;
            var salesListMock = new List<Sale>()
            {
                new Sale(1, 1, new DateTime(1980, 1, 1)),
                new Sale(2, 2, new DateTime(1980, 1, 1)),
                new Sale(3, 3, new DateTime(1980, 1, 1))
            };

            _salesServiceMock.Setup(x => x.GetSaleBySellerId(sellerId)).Returns(salesListMock);

            //Act
            var result = _salesController.GetSalesBySellerId(sellerId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetSalesByBuyerId_ShouldReturn_204NoContent()
        {
            //Arrange 
            int sellerId = 1;
            var salesListMock = new List<Sale>() { };

            _salesServiceMock.Setup(x => x.GetSaleByBuyerId(sellerId)).Returns(salesListMock);

            //Act
            var result = _salesController.GetSalesByBuyerId(sellerId);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetSalesByBuyerId_ShouldReturn_200Ok()
        {
            //Arrange
            int sellerId = 1;
            var salesListMock = new List<Sale>()
            {
                new Sale(1, 1, new DateTime(1980, 1, 1)),
                new Sale(2, 2, new DateTime(1980, 1, 1)),
                new Sale(3, 3, new DateTime(1980, 1, 1))
            };

            _salesServiceMock.Setup(x => x.GetSaleByBuyerId(sellerId)).Returns(salesListMock);

            //Act
            var result = _salesController.GetSalesByBuyerId(sellerId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void CreateSaleBySellerId_ShouldReturn_201Created()
        {
            //Arrange
            int userId = 1;
            var saleRequestMock = new SaleBySellerRequest(1, DateTime.Now);
            var saleMock = saleRequestMock.ConvertToEntity(userId);
            _salesServiceMock.Setup(x => x.CreateSaleByUserId(saleMock)).Returns(saleMock.Id);

            //Act
            var result = _salesController.CreateSaleBySellerId(userId, saleRequestMock);

            //Assert
            Assert.IsType<CreatedAtActionResult>(result);

        }

        //[Fact]
        //public void CreateSaleBySellerId_ShouldReturn_400BadRequest()
        //{
        //    //Arrange
        //    int userId = 0;
        //    var saleRequestMock = new SaleBySellerRequest(1, DateTime.Now);
        //    var saleMock = saleRequestMock.ConvertToEntity(userId);
        //    _salesServiceMock.Setup(x => x.CreateSaleByUserId(saleMock)).Throws(new ArgumentException("BuyerId não encontrado."));

        //    //Act
        //    var result = _salesController.CreateSaleBySellerId(userId, saleRequestMock);

        //    //Assert
        //    Assert.IsType<BadRequestObjectResult>(result);

        //}        

        [Theory]
        [InlineData(1, 1, 1)]
        public void UpdateUnitPrice_ShouldReturn_NoContent201(int saleId, int productId, decimal unitPrice)
        {
            //Arrange
            _salesServiceMock.Setup(m => m.UpdateUnitPrice(saleId, productId, unitPrice)).Verifiable();

            //Act
            var result = _salesController.UpdateUnitPrice(saleId, productId, unitPrice);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        public void UpdateUnitPrice_ShouldReturn_BadRequest400(int saleId, int productId, decimal unitPrice)
        {
            //Arrange
            _salesServiceMock.Setup(m => m.UpdateUnitPrice(saleId, productId, unitPrice)).Throws(new ArgumentException());

            //Act
            var result = _salesController.UpdateUnitPrice(saleId, productId, unitPrice);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        public void UpdateUnitPrice_ShouldReturn_NotFound404(int saleId, int productId, decimal unitPrice)
        {
            //Arrange
            _salesServiceMock.Setup(m => m.UpdateUnitPrice(saleId, productId, unitPrice)).Throws(new Exception());

            //Act
            var result = _salesController.UpdateUnitPrice(saleId, productId, unitPrice);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        public void UpdateAmount_ShouldReturn_NoContent201(int saleId, int productId, int amount)
        {
            //Arrange
            _salesServiceMock.Setup(m => m.UpdateAmount(saleId, productId, amount)).Verifiable();

            //Act
            var result = _salesController.UpdateAmount(saleId, productId, amount);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Theory]
        [InlineData(1, 1, 1, "saleId")]
        [InlineData(1, 1, 1, "productId")]
        public void UpdateAmount_ShouldReturn_NotFound404(int saleId, int productId, int amount, string paramName)
        {
            //Arrange
            var exception = new ArgumentException("Teste", paramName);
            _salesServiceMock.Setup(m => m.UpdateAmount(saleId, productId, amount)).Throws(exception);

            //Act
            var result = _salesController.UpdateAmount(saleId, productId, amount);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Theory]
        [InlineData(1, 1, 1, "Teste")]
        public void UpdateAmount_ShouldReturn_BadRequest400(int saleId, int productId, int amount, string paramName)
        {
            //Arrange
            var exception = new ArgumentException("Teste", paramName);
            _salesServiceMock.Setup(m => m.UpdateAmount(saleId, productId, amount)).Throws(exception);

            //Act
            var result = _salesController.UpdateAmount(saleId, productId, amount);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void GetDeliveryById_ShouldReturn_NoContent204()
        {
            //Arrange
            int id = 1;
            _salesServiceMock.Setup(m => m.GetDeliveryById(id)).Returns<Delivery>(null);

            //Act
            var result = _salesController.GetDeliveryById(id);

            //Assert   
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void GetDeliveryById_ShouldReturn_Ok200()
        {
            //Arrange
            int id = 2;
            var deliveryMock = new Delivery(id, 1, new DateTime(1980, 1, 1));
            _salesServiceMock.Setup(m => m.GetDeliveryById(id)).Returns(deliveryMock);

            //Act
            var result = _salesController.GetDeliveryById(id);

            //Assert   
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
