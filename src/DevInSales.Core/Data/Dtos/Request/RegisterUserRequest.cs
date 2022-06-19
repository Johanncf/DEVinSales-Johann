using System.ComponentModel.DataAnnotations;

namespace DevInSales.Core.Data.Dtos
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "O campo {0} � obrigat�rio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio")]
        [EmailAddress(ErrorMessage = "Endere�o de email inv�lido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio")]
        [MinLength(6, ErrorMessage = "A senha deve conter no m�nimo 6 caracteres")]
        [MaxLength(10, ErrorMessage = "A senha pode conter no m�ximo 10 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo {0} � obrigat�rio")]
        [Compare(nameof(Password), ErrorMessage = "Os campos de senha e confirma��o de senha devem ser iguais")]
        public string PasswordConfirmation { get; set; }
    }
}