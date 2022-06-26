using DevInSales.Core.Identity.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevInSales.Core.Data.Dtos
{
    public class SetRoleRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string RoleName { get; set; }
    }
}
