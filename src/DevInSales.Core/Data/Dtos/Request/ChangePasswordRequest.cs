using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Data.Dtos
{
    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Endereço de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string NewPassword { get; set; }
    }
}
