using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Subtask
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите описание подзадачи")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина описания подзадачи должна быть от 3 до 50 символов")]
        public string Description { get; set; }
    }
}