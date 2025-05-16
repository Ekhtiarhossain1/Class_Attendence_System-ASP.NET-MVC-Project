using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassAttendanceSystem.Models
{
    public class Section
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
