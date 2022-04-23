using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите описание задачи")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина описания задачи должна быть от 3 до 50 символов")]
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public Priority Priority { get; set; }
        public List<Subtask> Subtasks { get; set; }
        public bool IsFinished { get; set; }
        public DateTime FinishDate { get; set; }
        public List<Hashtag> Hashtags { get; set; }
        public string Comment { get; set; }
        public List<Attachment> Attachments { get; set; }

        public Task()
        {
            Subtasks = new List<Subtask>();
            Hashtags = new List<Hashtag>();
            Attachments = new List<Attachment>();
        }
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }
}