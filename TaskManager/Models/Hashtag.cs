using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Hashtag
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите хэштег")]
        [RegularExpression(@"^#[a-zA-Z0-9]+$", ErrorMessage = "Хэштег введён неправильно")]
        public string Value { get; set; }
    }
}