using System.ComponentModel.DataAnnotations;

namespace LivestockFarmManagement.Models
{
    public class UserAccount
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "กรุณากรอกชื่อผู้ใช้")]
        [Display(Name = "ชื่อผู้ใช้งาน")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณากรอกรหัสผ่าน")]
        [Display(Name = "รหัสผ่าน")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "ชื่อ-นามสกุล")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "สิทธิ์การใช้งาน")]
        public string Role { get; set; } = "Admin"; // Admin, Staff

        [Display(Name = "สถานะการใช้งาน")]
        public bool IsActive { get; set; } = true;
    }
}