using System.ComponentModel.DataAnnotations;

namespace LivestockFarmManagement.Models
{
    public class SpeciesBreed
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "กรุณากรอกประเภทสัตว์")]
        [Display(Name = "ประเภทสัตว์")]
        public string Category { get; set; } = string.Empty; // เช่น วัว, หมู, แพะ, แกะ

        [Required(ErrorMessage = "กรุณากรอกสายพันธุ์")]
        [Display(Name = "สายพันธุ์")]
        public string BreedName { get; set; } = string.Empty; // เช่น บราห์มัน, ลาร์จไวท์, แองกัส

        [Display(Name = "ระยะเวลาตั้งท้อง (วัน)")]
        public int GestationDays { get; set; } = 283; // ระยะเวลาตั้งท้องมาตรฐาน
    }
}