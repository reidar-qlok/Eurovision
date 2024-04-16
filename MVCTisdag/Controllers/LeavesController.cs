using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCTisdag.Data;
using MVCTisdag.Models;

namespace MVCTisdag.Controllers
{
    public class LeavesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LeavesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var leaves = _context.Leaves.Include(l => l.Employee).ToList();
            return View(leaves);
        }
        [HttpGet]
        public IActionResult Apply()
        {
            // Hämta alla anställd för att fylla i en dropdown lista sen
            ViewBag.Employees = _context.Employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = e.EmployeeName
            }).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Apply(Leave leave)
        {
            if (ModelState.IsValid)
            {
                _context.Leaves.Add(leave);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Employees = _context.Employees.Select(e => new SelectListItem
            {
                Value = e.EmployeeId.ToString(),
                Text = e.EmployeeName
            }).ToList();
            return View(leave);
        }
        [HttpGet]
        public IActionResult EditStatus(int id)
        {
            var leave = _context.Leaves.Include(l => l.Employee).FirstOrDefault(l => l.LeaveId == id);
            if (leave == null)
            {
                return NotFound();
            }
            return View(leave);

        }
        [HttpPost]
        public IActionResult EditStatus(Leave model)
        {
            var leave = _context.Leaves.Find(model.LeaveId);
            if (leave == null)
            {
                return NotFound();
            }

            leave.Status = model.Status;
            _context.SaveChanges();
            return RedirectToAction("Index");

            return View(model);
        }
    }
}
