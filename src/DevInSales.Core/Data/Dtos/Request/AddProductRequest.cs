using System.ComponentModel.DataAnnotations;

namespace DevInSales.Core.Data.Dtos
{
    public class AddProductRequest 
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public decimal SuggestedPrice { get; set; }
    }
}
