using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models
{
    public class Tasks
    {
        [Key]
        public int TaskID { get; set; }

        [Required]
        [MaxLength(24)]
        public string TaskName { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        public bool IsDone { get; set; } = false;

        [Required]
        [Range(1,5)]
        public int Priority { get; set; }
    }
}