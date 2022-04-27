using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Длина логина должна быть от 5 до 20 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Длина пароля должна быть от 8 до 30 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        #region Дополнительные свойства для валидации
        [NotMapped]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Введите логин")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Длина логина должна быть от 5 до 20 символов")]
        public string SignInLogin { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Длина пароля должна быть от 8 до 30 символов")]
        [DataType(DataType.Password)]
        public string SignInPassword { get; set; }
        #endregion

        public List<Task> Tasks { get; set; }

        public User()
        {
            Tasks = new List<Task>();
        }
    }
}