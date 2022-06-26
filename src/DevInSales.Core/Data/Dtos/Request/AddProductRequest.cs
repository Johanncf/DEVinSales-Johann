using System.ComponentModel.DataAnnotations;

namespace DevInSales.Core.Data.Dtos
{
    public class AddProductRequest 
    {
        [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio.")]
        public decimal SuggestedPrice { get; set; }
    }
}
