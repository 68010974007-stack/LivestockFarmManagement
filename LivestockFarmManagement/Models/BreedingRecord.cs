using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LivestockFarmManagement.Models
{
    public class BreedingRecord
    {
        public int Id { get; set; }

        [Required]
        public int LivestockId { get; set; }

        public DateTime MateDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public string? ParentTag { get; set; }
        public string Status { get; set; } = "PREGNANT";

        // Navigation Property
        public virtual Livestock? Livestock { get; set; }

        // --- Property สำรองเพื่อแก้ Error ใน Views (Details.cshtml) ---
        [NotMapped]
        public DateTime MatingDate
        {
            get => MateDate;
            set => MateDate = value;
        }

        [NotMapped]
        public DateTime ExpectedBirthDate
        {
            get => DueDate;
            set => DueDate = value;
        }

        [NotMapped]
        public string? FatherBreed
        {
            get => ParentTag;
            set => ParentTag = value;
        }
    }
}