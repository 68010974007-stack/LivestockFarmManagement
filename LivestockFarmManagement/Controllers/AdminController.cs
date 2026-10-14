using Microsoft.AspNetCore.Mvc;
using LivestockFarmManagement.Data;
using LivestockFarmManagement.Models;
using System.Linq;

namespace LivestockFarmManagement.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Admin หรือ /Admin/Index
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.TotalLivestocks = _context.Livestocks.Count();
            ViewBag.TotalUsers = _context.UserAccounts.Count();
            ViewBag.TotalBreeding = _context.BreedingRecords.Count();
            ViewBag.TotalVaccines = _context.VaccineRecords.Count();

            // คำนวณรายรับรวม
            ViewBag.TotalIncome = _context.FinancialTransactions
                .Where(t => t.TransactionType == "INCOME" || t.TransactionType == "รายรับ")
                .Sum(t => (decimal?)t.Amount) ?? 0;

            // คำนวณรายจ่ายรวม
            ViewBag.TotalExpense = _context.FinancialTransactions
                .Where(t => t.TransactionType == "EXPENSE" || t.TransactionType == "รายจ่าย")
                .Sum(t => (decimal?)t.Amount) ?? 0;

            return View();
        }

        // GET: /Admin/Users
        public IActionResult Users()
        {
            var users = _context.UserAccounts.ToList();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                _context.UserAccounts.Add(user);
                _context.SaveChanges();
                return RedirectToAction(nameof(Users));
            }
            return View("Users", _context.UserAccounts.ToList());
        }

        public IActionResult DeleteUser(int id)
        {
            var user = _context.UserAccounts.Find(id);
            if (user != null)
            {
                _context.UserAccounts.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Users));
        }

        // GET: /Admin/SpeciesBreeds
        public IActionResult SpeciesBreeds()
        {
            var species = _context.SpeciesBreeds.ToList();
            return View(species);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSpeciesBreed(SpeciesBreed item)
        {
            if (ModelState.IsValid)
            {
                _context.SpeciesBreeds.Add(item);
                _context.SaveChanges();
                return RedirectToAction(nameof(SpeciesBreeds));
            }
            return View("SpeciesBreeds", _context.SpeciesBreeds.ToList());
        }

        public IActionResult DeleteSpeciesBreed(int id)
        {
            var item = _context.SpeciesBreeds.Find(id);
            if (item != null)
            {
                _context.SpeciesBreeds.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(SpeciesBreeds));
        }
    }
}