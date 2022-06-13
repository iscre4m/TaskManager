using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите путь к файлу")]
        [RegularExpression(@"^files\/([a-zA-Z0-9А-я]+\/)*[a-zA-Z0-9А-я]+\.[a-z]{1,4}$")]
        public string Path { get; set; }
    }
}