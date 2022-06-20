using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Укажите логин")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Длина логина должна быть от 4 до 20 символов")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Длина пароля должна быть от 8 до 30 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}