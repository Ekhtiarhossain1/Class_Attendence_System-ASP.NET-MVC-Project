using Microsoft.AspNetCore.Mvc;
using ClassAttendanceSystem.Data;
using ClassAttendanceSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClassAttendanceSystem.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ClassAttendanceContext _context;

        public AttendanceController(ClassAttendanceContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.IsAuthenticated = HttpContext.Session.GetString("UserRole") != null;
        }

        public IActionResult TakeAttendance()
        {
            var sections = _context.Sections.ToList();
            return View("SelectSection", sections);
        }

        [HttpPost]
        public IActionResult SelectSection(int sectionId)
        {
            var students = _context.Students.Where(s => s.SectionId == sectionId).ToList();
            ViewBag.SectionId = sectionId;
            return View("TakeAttendance", students);
        }

        [HttpPost]
        public IActionResult TakeAttendance(DateTime date, Dictionary<int, bool> attendanceData)
        {
            foreach (var entry in attendanceData)
            {
                var attendance = new Attendance
                {
                    Date = date,
                    StudentId = entry.Key,
                    IsPresent = entry.Value
                };
                _context.Attendances.Add(attendance);
            }
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Teacher");
        }

        public IActionResult ViewAttendance()
        {
            var sectionList = _context.Sections.ToList();
            ViewBag.Sections = sectionList;
            return View();
        }

        [HttpPost]
        public IActionResult ViewAttendance(int sectionId)
        {
            var attendanceRecords = _context.Attendances
                .Include(a => a.Student)
                .Where(a => a.Student != null && a.Student.SectionId == sectionId)
                .GroupBy(a => a.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToList();
            ViewBag.SectionId = sectionId;
            ViewBag.Sections = _context.Sections.ToList();
            return View("ViewAttendance", attendanceRecords);
        }

        public IActionResult EditAttendance(DateTime date, int sectionId)
        {
            // Get all students in the section
            var allStudents = _context.Students.Where(s => s.SectionId == sectionId).ToList();
            // Get attendance records for the date and section
            var attendanceRecords = _context.Attendances
                .Include(a => a.Student)
                .Where(a => a.Date == date && a.Student != null && a.Student.SectionId == sectionId)
                .ToList();
            // For students without an attendance record, add a record as absent
            foreach (var student in allStudents)
            {
                if (!attendanceRecords.Any(a => a.StudentId == student.Id))
                {
                    attendanceRecords.Add(new Attendance
                    {
                        StudentId = student.Id,
                        Student = student,
                        Date = date,
                        IsPresent = false
                    });
                }
            }
            // Sort by StudentId as string for consistency, handle possible nulls
            attendanceRecords = attendanceRecords.OrderBy(a => a.Student != null && a.Student.StudentId != null ? a.Student.StudentId : "").ToList();
            ViewBag.SectionId = sectionId;
            ViewBag.Date = date;
            return View(attendanceRecords);
        }

        [HttpPost]
        public IActionResult EditAttendance(List<Attendance> updatedRecords, int sectionId, DateTime date)
        {
            foreach (var record in updatedRecords)
            {
                var existingRecord = _context.Attendances.Find(record.Id);
                if (existingRecord != null)
                {
                    existingRecord.IsPresent = record.IsPresent;
                }
            }
            _context.SaveChanges();
            return RedirectToAction("ViewAttendance", new { sectionId });
        }
    }
}

// How to Run

// From the root directory, always use:
// dotnet run --project .\ClassAttendanceSystem\ClassAttendanceSystem.csproj
