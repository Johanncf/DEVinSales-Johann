using System.ComponentModel.DataAnnotations;

namespace DevInSales.Core.Data.Dtos
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "Endereço de email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [MinLength(6, ErrorMessage = "A senha deve conter no mínimo 6 caracteres")]
        [MaxLength(10, ErrorMessage = "A senha pode conter no máximo 10 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Compare(nameof(Password), ErrorMessage = "Os campos de senha e confirmação de senha devem ser iguais")]
        public string PasswordConfirmation { get; set; }
    }
}