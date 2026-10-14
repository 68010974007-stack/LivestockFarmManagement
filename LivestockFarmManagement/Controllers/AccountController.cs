using Microsoft.AspNetCore.Mvc;
using LivestockFarmManagement.Models;

namespace LivestockFarmManagement.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // ตรวจสอบชื่อผู้ใช้และรหัสผ่าน (ตัวอย่างกำหนดไว้เป็น admin / 1234)
                if (model.Username == "admin" && model.Password == "1234")
                {
                    // เข้าสู่ระบบสำเร็จ นำไปยังหน้าจัดการปศุสัตว์
                    return RedirectToAction("Index", "Livestock");
                }

                ModelState.AddModelError(string.Empty, "ชื่อผู้ใช้หรือรหัสผ่านไม่ถูกต้อง");
            }

            return View(model);
        }

        // 🚪 ฟังก์ชันออกจากระบบ (ส่งผู้ใช้กลับไปหน้า Login ทันที)
        [HttpPost]
        [HttpGet]
        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}