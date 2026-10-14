using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivestockFarmManagement.Models
{
    public class Livestock
    {
        public int Id { get; set; }

        [Display(Name = "ชื่อ/รหัสสัตว์")]
        public string? Name { get; set; }

        [Display(Name = "หมายเลขแท็ก")]
        public string TagCode { get; set; } = string.Empty;

        [Display(Name = "ประเภท")]
        public string Category { get; set; } = string.Empty;

        [Display(Name = "เพศ")]
        public string Gender { get; set; } = string.Empty;

        [Display(Name = "สายพันธุ์")]
        public string Breed { get; set; } = string.Empty;

        [Display(Name = "วันเกิด")]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        [Display(Name = "สถานะ")]
        public string Status { get; set; } = "HEALTHY";

        [Display(Name = "สถานะสุขภาพ")]
        public string HealthStatus { get; set; } = "สมบูรณ์ดี";

        [Display(Name = "น้ำหนัก (กก.)")]
        public decimal Weight { get; set; }

        [Display(Name = "มูลค่าประเมิน")]
        public decimal EstimatedValue { get; set; }

        [Display(Name = "คอก/โรงเรือน")]
        public string? PenNumber { get; set; }

        [Display(Name = "รูปภาพ URL")]
        public string? ImageUrl { get; set; }

        [Display(Name = "บันทึกเพิ่มเติม")]
        public string? Notes { get; set; }

        // --- Alias Property รองรับการเรียกชื่อเดิมใน Views ---
        [NotMapped]
        public string Code
        {
            get => string.IsNullOrWhiteSpace(TagCode) ? $"ANIMAL-{Id:D3}" : TagCode;
            set => TagCode = value;
        }

        [NotMapped]
        public string TagNumber
        {
            get => TagCode;
            set => TagCode = value;
        }

        // Navigation Properties
        public virtual ICollection<VaccineRecord>? VaccineRecords { get; set; }
        public virtual ICollection<BreedingRecord>? BreedingRecords { get; set; }
    }
}