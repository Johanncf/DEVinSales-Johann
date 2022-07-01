using DevInSales.Api.Controllers;
using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Entities;
using DevInSales.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Api.Tests.Controllers
{
    public class ProductControllerTest
    {
        private Mock<IProductService> _productServiceMock;
        private ProductController _productController;

        public ProductControllerTest()
        {
            _productServiceMock = new Mock<IProductService>();
            _productController = new ProductController(_productServiceMock.Object);
        }

        [Fact]
        public void ObterProdutoPorId_ShouldReturn_200Ok()
        {
            //Arrange
            var productId = 1;
            var product = new Product(productId, "Teste", 10);

            _productServiceMock.Setup(x => x.ObterProductPorId(productId)).Returns(product);

            //Act
            var result = _productController.ObterProdutoPorId(productId);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void ObterProdutoPorId_ShouldReturn_404NotFound()
        {
            //Arrange
            var productId = 1;
            var product = new Product(productId, "Teste", 10);

            _productServiceMock.Setup(x => x.ObterProductPorId(productId)).Returns<Product>(null);
            
            //Act
            var result = _productController.ObterProdutoPorId(productId);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AtualizarProduto_ShouldReturn_404NotFound()
        {
            //Arrange
            int idMock = 10;
            var product = new AddProductRequest
            {
                Name = "Teste",
                SuggestedPrice = 10
            }; 
            _productServiceMock.Setup(x => x.ObterProductPorId(idMock)).Returns<Product>(null);

            //Act
            var result = _productController.AtualizarProduto(product, idMock);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Theory]
        [InlineData("string")]
        [InlineData("StRiNg")]
        [InlineData("Teste")]        
        public void AtualizarProduto_ShouldReturn_400BadRequest(string modelName)
        {
            //Arrange
            int idMock = 10;
            var product = new AddProductRequest
            {
                Name = modelName,
                SuggestedPrice = 10
            };
            _productServiceMock.Setup(x => x.ObterProductPorId(idMock)).Returns(new Product("Teste", 10));
            if (modelName == "Teste")
            _productServiceMock.Setup(x => x.ProdutoExiste(product.Name)).Returns(true);

            //Act
            var result = _productController.AtualizarProduto(product, idMock);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
