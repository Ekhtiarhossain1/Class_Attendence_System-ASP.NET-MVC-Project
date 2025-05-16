using System.ComponentModel.DataAnnotations;

namespace ClassAttendanceSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string StudentId { get; set; } = string.Empty;

        public int? SectionId { get; set; }
        public Section? Section { get; set; }
    }
}
