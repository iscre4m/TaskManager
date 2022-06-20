using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Hashtag
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите хэштег")]
        public string Value { get; set; }
    }
}