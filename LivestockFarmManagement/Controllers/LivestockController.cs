using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LivestockFarmManagement.Data;
using LivestockFarmManagement.Models;

namespace LivestockFarmManagement.Controllers
{
    public class LivestockController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LivestockController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Livestock (แสดงรายการทั้งหมด)
        public async Task<IActionResult> Index()
        {
            return View(await _context.Livestocks.ToListAsync());
        }

        // GET: Livestock/Details/5 (ดูรายละเอียด)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var livestock = await _context.Livestocks
                .Include(l => l.VaccineRecords)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (livestock == null) return NotFound();

            return View(livestock);
        }

        // GET: Livestock/Create (หน้าฟอร์มเพิ่มข้อมูล)
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livestock/Create (บันทึกข้อมูลใหม่)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Livestock livestock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livestock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(livestock);
        }

        // GET: Livestock/Edit/5 (หน้าฟอร์มแก้ไข)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var livestock = await _context.Livestocks.FindAsync(id);
            if (livestock == null) return NotFound();

            return View(livestock);
        }

        // POST: Livestock/Edit/5 (บันทึกการแก้ไข)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Livestock livestock)
        {
            if (id != livestock.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livestock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivestockExists(livestock.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(livestock);
        }

        // GET: Livestock/Delete/5 (หน้ายืนยันการลบ)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var livestock = await _context.Livestocks
                .FirstOrDefaultAsync(m => m.Id == id);

            if (livestock == null) return NotFound();

            return View(livestock);
        }

        // POST: Livestock/Delete/5 (ยืนยันลบข้อมูลจริง)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livestock = await _context.Livestocks.FindAsync(id);
            if (livestock != null)
            {
                _context.Livestocks.Remove(livestock);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool LivestockExists(int id)
        {
            return _context.Livestocks.Any(e => e.Id == id);
        }
        // GET: Livestock/Finance (หน้าบัญชีรายรับ-รายจ่าย)
        public async Task<IActionResult> Finance()
        {
            var transactions = await _context.FinancialTransactions
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
            return View(transactions);
        }

        // POST: Livestock/AddTransaction
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTransaction(FinancialTransaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.FinancialTransactions.Add(transaction);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Finance));
        }

        // POST: Livestock/AddVaccine
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVaccine(VaccineRecord vaccine)
        {
            if (ModelState.IsValid)
            {
                _context.VaccineRecords.Add(vaccine);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Details), new { id = vaccine.LivestockId });
        }
    }
}