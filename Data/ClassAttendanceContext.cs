using Microsoft.EntityFrameworkCore;
using ClassAttendanceSystem.Models;

namespace ClassAttendanceSystem.Data
{
    public class ClassAttendanceContext : DbContext
    {
        public ClassAttendanceContext(DbContextOptions<ClassAttendanceContext> options) : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
