using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ClassAttendanceSystem.Data;
using ClassAttendanceSystem.Models;

namespace ClassAttendanceSystem.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ClassAttendanceContext _context;

        public TeacherController(ClassAttendanceContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.IsAuthenticated = HttpContext.Session.GetString("UserRole") != null;
        }

        public IActionResult Login()
        {
            // Redirect to Home/Login (navbar login) instead of showing this page
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Redirect to Home/Login (navbar login) instead of showing this page
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult CreateTeacher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTeacher(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Teachers.Add(teacher);
                _context.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View(teacher);
        }
    }
}
