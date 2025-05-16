using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ClassAttendanceSystem.Data;
using ClassAttendanceSystem.Models;

namespace ClassAttendanceSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ClassAttendanceContext _context;

        public StudentController(ClassAttendanceContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.IsAuthenticated = HttpContext.Session.GetString("UserRole") != null;
        }

        public IActionResult CreateStudent()
        {
            ViewBag.Sections = _context.Sections.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student, string? newSectionName, int? selectedSectionId)
        {
            if (!string.IsNullOrWhiteSpace(newSectionName))
            {
                // Create new section if a new name is provided
                var section = new Section { Name = newSectionName };
                _context.Sections.Add(section);
                _context.SaveChanges();
                student.SectionId = section.Id;
            }
            else if (selectedSectionId.HasValue)
            {
                student.SectionId = selectedSectionId.Value;
            }

            if (ModelState.IsValid)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Teacher");
            }
            ViewBag.Sections = _context.Sections.ToList();
            return View(student);
        }

        public IActionResult RemoveStudent()
        {
            ViewBag.Sections = _context.Sections.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult RemoveStudent(int sectionId)
        {
            var students = _context.Students.Where(s => s.SectionId == sectionId).ToList();
            ViewBag.Sections = _context.Sections.ToList();
            ViewBag.SelectedSectionId = sectionId;
            return View("RemoveStudent", students);
        }

        [HttpPost]
        public IActionResult DeleteStudent(int id, int sectionId)
        {
            var student = _context.Students.Find(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            // Stay on the same section after deletion
            var students = _context.Students.Where(s => s.SectionId == sectionId).ToList();
            ViewBag.Sections = _context.Sections.ToList();
            ViewBag.SelectedSectionId = sectionId;
            return View("RemoveStudent", students);
        }
    }
}
