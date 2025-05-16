using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using ClassAttendanceSystem.Data;
using ClassAttendanceSystem.Models;

namespace ClassAttendanceSystem.Controllers
{
    public class SectionController : Controller
    {
        private readonly ClassAttendanceContext _context;

        public SectionController(ClassAttendanceContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.IsAuthenticated = HttpContext.Session.GetString("UserRole") != null;
        }

        public IActionResult Index()
        {
            var sections = _context.Sections.ToList();
            return View(sections);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Sections.Add(section);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(section);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var section = _context.Sections.Find(id);
            if (section != null)
            {
                _context.Sections.Remove(section);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
