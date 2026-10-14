using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivestockFarmManagement.Models
{
    public class FinancialTransaction
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "ประเภทรายการ")]
        public string TransactionType { get; set; } = "INCOME"; // INCOME หรือ EXPENSE

        // --- Alias Property สำหรับรองรับการเรียก .Type ---
        [NotMapped]
        public string Type
        {
            get => TransactionType;
            set => TransactionType = value;
        }

        [Display(Name = "หมวดหมู่")]
        public string? CategoryName { get; set; } = "ทั่วไป";

        // --- Alias Property สำหรับรองรับการเรียก .Category ---
        [NotMapped]
        public string Category
        {
            get => CategoryName ?? "ทั่วไป";
            set => CategoryName = value;
        }

        [Display(Name = "รายละเอียด")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "จำนวนเงิน")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Display(Name = "วันที่ทำรายการ")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        [Display(Name = "หมายเหตุ")]
        public string? Notes { get; set; }
    }
}