using System;
using System.ComponentModel.DataAnnotations;

namespace ClassAttendanceSystem.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int StudentId { get; set; }

        public bool IsPresent { get; set; }

        public Student? Student { get; set; }
    }
}
