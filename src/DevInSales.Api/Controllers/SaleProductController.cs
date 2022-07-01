using DevInSales.Core.Data.Dtos;
using DevInSales.Core.Identity.Constants;
using DevInSales.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevInSales.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SaleProductController : ControllerBase
    {
        private readonly ISaleProductService _saleProductService;

        public SaleProductController(ISaleProductService saleProductService)
        {
            _saleProductService = saleProductService;
        }

        // Endpoint criado apenas para servir como path do POST {saleId}/item
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("saleById/item")]
        public IActionResult GetSaleProductById(int saleProductId)
        {
            var saleProductDTO = _saleProductService.GetSaleProductById(saleProductId);
            if (saleProductDTO == null)
                return NotFound();

            return Ok(saleProductDTO);
        }

        /// <summary>
        /// Cadastra um produto em uma venda.
        /// </summary>
        ///<returns> Retorna um id da tabela saleProduct.</returns>
        /// <response code="201">Criado com sucesso.</response>
        /// <response code="400">Bad Request, caso não seja enviado um productId ou quando a quantidade/preço enviados forem menor ou igual a zero.</response>
        /// <response code="404">Not Found, caso o productId ou o saleId não existam.</response>
        [HttpPost("{saleId}/item")]
        [Authorize(Roles = $"{Roles.Admin}, {Roles.Gerente}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateSaleProduct(int saleId, SaleProductRequest saleProduct)
        {
            try
            {
                if (saleProduct.ProductId <= 0)
                    return BadRequest();

                if (saleProduct.Amount == null)
                    saleProduct.Amount = 1;

                var id = _saleProductService.CreateSaleProduct(saleId, saleProduct);
                return CreatedAtAction(nameof(GetSaleProductById), new { saleProductId = id }, id);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.Contains("não encontrado."))
                    return NotFound();

                if (ex.Message.Contains("não podem ser negativos."))
                    return BadRequest();

                return BadRequest();
            }
        }
    }
}
