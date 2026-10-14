using System.ComponentModel.DataAnnotations;

namespace LivestockFarmManagement.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "กรุณากรอกชื่อผู้ใช้หรืออีเมล")]
        [Display(Name = "ชื่อผู้ใช้ / อีเมล")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "กรุณากรอกรหัสผ่าน")]
        [DataType(DataType.Password)]
        [Display(Name = "รหัสผ่าน")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "จดจำฉันไว้ในระบบ")]
        public bool RememberMe { get; set; }
    }
}