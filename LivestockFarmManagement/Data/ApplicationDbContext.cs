using Microsoft.EntityFrameworkCore;
using LivestockFarmManagement.Models;

namespace LivestockFarmManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 🐄 ตารางข้อมูลสัตว์เลี้ยงหลัก
        public DbSet<Livestock> Livestocks { get; set; }

        // 💉 ตารางบันทึกประวัติสุขภาพและการฉีดวัคซีน
        public DbSet<VaccineRecord> VaccineRecords { get; set; }

        // 🧬 ตารางบันทึกประวัติการผสมพันธุ์และการคาดการณ์วันคลอด
        public DbSet<BreedingRecord> BreedingRecords { get; set; }

        // 💰 ตารางบันทึกรายรับ-รายจ่ายของฟาร์ม
        public DbSet<FinancialTransaction> FinancialTransactions { get; set; }

        // 👤 ตารางจัดการผู้ใช้งาน (User Management)
        public DbSet<UserAccount> UserAccounts { get; set; }

        // 🏷️ ตารางจัดการประเภทและสายพันธุ์สัตว์ (Species & Breed Master Data)
        public DbSet<SpeciesBreed> SpeciesBreeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ตั้งค่าความสัมพันธ์ (One-to-Many) ระหว่าง Livestock กับ VaccineRecords
            modelBuilder.Entity<VaccineRecord>()
                .HasOne(v => v.Livestock)
                .WithMany(l => l.VaccineRecords)
                .HasForeignKey(v => v.LivestockId)
                .OnDelete(DeleteBehavior.Cascade);

            // ตั้งค่าความสัมพันธ์ (One-to-Many) ระหว่าง Livestock กับ BreedingRecords
            modelBuilder.Entity<BreedingRecord>()
                .HasOne(b => b.Livestock)
                .WithMany(l => l.BreedingRecords)
                .HasForeignKey(b => b.LivestockId)
                .OnDelete(DeleteBehavior.Cascade);

            // กำหนดชนิดข้อมูลตัวเลขทศนิยม (Decimal)
            modelBuilder.Entity<Livestock>()
                .Property(l => l.Weight)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Livestock>()
                .Property(l => l.EstimatedValue)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<FinancialTransaction>()
                .Property(f => f.Amount)
                .HasColumnType("decimal(18,2)");
        }
    }
}