using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ClassAttendanceSystem.Models;
using ClassAttendanceSystem.Data;

namespace ClassAttendanceSystem.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ClassAttendanceContext _context;

    public HomeController(ILogger<HomeController> logger, ClassAttendanceContext context)
    {
        _logger = logger;
        _context = context;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        // Set authentication status for layout
        ViewBag.IsAuthenticated = HttpContext.Session.GetString("UserRole") != null;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password, string role)
    {
        if (role == "Admin")
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == username && a.Password == password);
            if (admin != null)
            {
                HttpContext.Session.SetString("UserRole", "Admin");
                return RedirectToAction("AdminDashboard");
            }
            ViewBag.Error = "Invalid admin credentials";
            return View();
        }
        else if (role == "Teacher")
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Username == username && t.Password == password);
            if (teacher != null)
            {
                HttpContext.Session.SetString("UserRole", "Teacher");
                return RedirectToAction("Dashboard", "Teacher");
            }
            ViewBag.Error = "Invalid teacher credentials";
            return View();
        }
        ViewBag.Error = "Invalid role selected";
        return View();
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult AdminDashboard()
    {
        // Placeholder admin dashboard
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
