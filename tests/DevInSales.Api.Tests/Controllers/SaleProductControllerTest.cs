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
    public class SaleProductControllerTest
    {
        private Mock<ISaleProductService> _saleProductServiceMock;
        private SaleProductController _saleProductController;
        public SaleProductControllerTest()
        {
            _saleProductServiceMock = new Mock<ISaleProductService>();
            _saleProductController = new SaleProductController(_saleProductServiceMock.Object);
        }

        [Fact]
        public void GetSaleProductById_ShouldReturn_200OkResult()
        {
            //Arrange
            int id = 1;
            var saleProductResponseMock = new SaleProductResponse("Teste", 1, 10);
            
            _saleProductServiceMock.Setup(s => s.GetSaleProductById(id)).Returns(saleProductResponseMock);

            //Act
            var result = _saleProductController.GetSaleProductById(id);

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public void GetSaleProductById_ShouldReturn_404NotFoundResult()
        {
            //Arrange
            int id = 1;
            _saleProductServiceMock.Setup(s => s.GetSaleProductById(id)).Returns<SaleProductResponse>(null);

            //Act
            var result = _saleProductController.GetSaleProductById(id);

            //Assert
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }

        [Theory]
        [InlineData(0, 1, 1)]
        [InlineData(-3, 1, 1)]
        [InlineData(1, -1, 1)]
        [InlineData(1, 1, -1)]
        public void CreateSaleProduct_ShouldReturn_400BadRequest(int productId, decimal preco, int quantidade)
        {
            //Arrange
            var saleProductRequestMock = new SaleProductRequest
            {
                ProductId = productId,
                UnitPrice = preco,
                Amount = quantidade
            };

            _saleProductServiceMock
                .Setup(m => m.CreateSaleProduct(1, saleProductRequestMock))
                .Throws(new ArgumentException("não podem ser negativos."));

            //Act
            var result = _saleProductController.CreateSaleProduct(1, saleProductRequestMock);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Theory]
        [InlineData(1, 1, 1)]
        public void CreateSaleProduct_ShouldReturn_404NotFound(int productId, decimal preco, int quantidade)
        {
            //Arrange
            var saleProductRequestMock = new SaleProductRequest
            {
                ProductId = productId,
                UnitPrice = preco,
                Amount = quantidade
            };

            _saleProductServiceMock
                .Setup(m => m.CreateSaleProduct(1, saleProductRequestMock))
                .Throws(new ArgumentException("não encontrado."));

            //Act
            var result = _saleProductController.CreateSaleProduct(1, saleProductRequestMock);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void CreateSaleProduct_ShouldSet_SaleProductAmount_To_1()
        {
            //Arrange
            var saleProductRequestMock = new SaleProductRequest
            {
                ProductId = 1,
                UnitPrice = 1
            };
            
            //Act
            _saleProductController.CreateSaleProduct(1, saleProductRequestMock);

            //Assert
            Assert.Equal(1, saleProductRequestMock.Amount);
        }
    }
}
