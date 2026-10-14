using Microsoft.AspNetCore.Mvc;
using LivestockFarmManagement.Data;
using LivestockFarmManagement.Models;
using System;
using System.Linq;

namespace LivestockFarmManagement.Controllers
{
    public class BreedingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BreedingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // หน้าแสดงรายการผสมพันธุ์ทั้งหมด
        public IActionResult Index()
        {
            var records = _context.BreedingRecords.ToList();
            // 📌 แก้ไขจุดนี้: ใช้ Livestocks (มี s) ให้ตรงกับ DbContext
            ViewBag.LivestockList = _context.Livestocks.ToList();
            return View(records);
        }

        // บันทึกข้อมูลการผสมพันธุ์และคำนวณวันคลอดให้อัตโนมัติ
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int livestockId, DateTime mateDate, string? parentTag)
        {
            // 📌 แก้ไขจุดนี้: ใช้ Livestocks (มี s) ให้ตรงกับ DbContext
            var animal = _context.Livestocks.Find(livestockId);
            int gestationDays = 283; // ค่าเริ่มต้น (วัว ~283 วัน)

            if (animal != null)
            {
                if (animal.Category == "หมู") gestationDays = 114; // หมู ~114 วัน
                else if (animal.Category == "แพะ" || animal.Category == "แกะ") gestationDays = 150;
            }

            var record = new BreedingRecord
            {
                LivestockId = livestockId,
                MateDate = mateDate,
                DueDate = mateDate.AddDays(gestationDays),
                ParentTag = string.IsNullOrWhiteSpace(parentTag) ? "-" : parentTag,
                Status = "ตั้งท้อง (PREGNANT)"
            };

            _context.BreedingRecords.Add(record);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // ลบบันทึกการผสมพันธุ์
        public IActionResult Delete(int id)
        {
            var record = _context.BreedingRecords.Find(id);
            if (record != null)
            {
                _context.BreedingRecords.Remove(record);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}