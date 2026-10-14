using LivestockFarmManagement.Models;

namespace LivestockFarmManagement.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // 📌 ลบข้อมูลเก่าที่มีรูปสลับกันออกทั้งหมด
            if (context.Livestocks.Any())
            {
                context.Livestocks.RemoveRange(context.Livestocks);
                context.SaveChanges();
            }

            var livestocks = new Livestock[]
            {
                // 🐄 โคนม (Dairy Cattle)
                new Livestock { Code="COW-001", Name="แม่มะลิ", Category="โคนม", Breed="โฮลสไตน์ ฟรีเชียน", Gender="เมีย", BirthDate=DateTime.Parse("2022-03-15"), Weight=480.50m, HealthStatus="สมบูรณ์ดี", PenNumber="คอก A1", EstimatedValue=55000m, ImageUrl="1.jpg", Notes="แม่พันธุ์โคนม ให้ผลผลิตนม 25 ลิตร/วัน" },
                new Livestock { Code="COW-002", Name="สร้อยฟ้า", Category="โคนม", Breed="เจอร์ซีย์", Gender="เมีย", BirthDate=DateTime.Parse("2022-07-20"), Weight=420.00m, HealthStatus="สมบูรณ์ดี", PenNumber="คอก A2", EstimatedValue=48000m, ImageUrl="https://images.unsplash.com/photo-1527153857715-3908f2bae5e8?w=800&auto=format&fit=crop", Notes="เปอร์เซ็นต์ไขมันในนมสูง คุณภาพดีเยี่ยม" },

                // 🐂 โคเนื้อ (Beef Cattle)
                new Livestock { Code="BEEF-001", Name="เจ้าขุน", Category="โคเนื้อ", Breed="อเมริกันบราห์มัน", Gender="ผู้", BirthDate=DateTime.Parse("2021-08-10"), Weight=650.00m, HealthStatus="สมบูรณ์ดี", PenNumber="คอก B1", EstimatedValue=85000m, ImageUrl="https://images.unsplash.com/photo-1546445317-29f4545f9d52?w=800&auto=format&fit=crop", Notes="พ่อพันธุ์โครงสร้างใหญ่ กล้ามเนื้อแน่น" },
                new Livestock { Code="BEEF-002", Name="กระทิงเหล็ก", Category="โคเนื้อ", Breed="แองกัส", Gender="ผู้", BirthDate=DateTime.Parse("2022-01-05"), Weight=580.00m, HealthStatus="สมบูรณ์ดี", PenNumber="คอก B2", EstimatedValue=92000m, ImageUrl="https://images.unsplash.com/photo-1500595046743-cd271d694d30?w=800&auto=format&fit=crop", Notes="สายพันธุ์เนื้อคุณภาพสูง ลายไขมันแทรกดี" },

                // 🐖 สุกร (Swine)
                new Livestock { Code="PIG-001", Name="เจ้าหมูอ้วน", Category="สุกร", Breed="ลาร์จไวท์", Gender="ผู้", BirthDate=DateTime.Parse("2023-01-20"), Weight=115.00m, HealthStatus="สมบูรณ์ดี", PenNumber="โรงเรือน C1", EstimatedValue=9500m, ImageUrl="https://images.unsplash.com/photo-1516467508483-a7212febe31a?w=800&auto=format&fit=crop", Notes="สุกรขุน สุขภาพแข็งแรง พร้อมส่งตลาด" },
                new Livestock { Code="PIG-002", Name="ชมพูพันธุ์ดี", Category="สุกร", Breed="แลนด์เรซ", Gender="เมีย", BirthDate=DateTime.Parse("2023-02-14"), Weight=108.00m, HealthStatus="สมบูรณ์ดี", PenNumber="โรงเรือน C2", EstimatedValue=10500m, ImageUrl="https://images.unsplash.com/photo-1596733430284-f7437764b1a9?w=800&auto=format&fit=crop", Notes="แม่พันธุ์สุกร ให้ลูกดก" },

                // 🐐 แพะ/แกะ (Goats & Sheep)
                new Livestock { Code="GOAT-001", Name="ทองดี", Category="แพะ/แกะ", Breed="แองโกลนูเบียน", Gender="เมีย", BirthDate=DateTime.Parse("2023-05-12"), Weight=45.00m, HealthStatus="เฝ้าระวัง", PenNumber="คอก D1", EstimatedValue=12000m, ImageUrl="https://images.unsplash.com/photo-1524024973431-2ad916746881?w=800&auto=format&fit=crop", Notes="ซึมเล็กน้อย สัตวแพทย์ติดตามอาการใกล้ชิด" },
                new Livestock { Code="SHEEP-001", Name="ปุยฝ้าย", Category="แพะ/แกะ", Breed="คาทาดิน", Gender="เมีย", BirthDate=DateTime.Parse("2023-06-01"), Weight=38.00m, HealthStatus="สมบูรณ์ดี", PenNumber="คอก D2", EstimatedValue=8500m, ImageUrl="https://images.unsplash.com/photo-1484557052118-f32bd25b45b5?w=800&auto=format&fit=crop", Notes="แกะสายพันธุ์เนื้อ ไม่ต้องตัดขน" },

                // 🐔 สัตว์ปีก (Poultry)
                new Livestock { Code="CHICK-001", Name="ฝูงไก่ไข่ A", Category="สัตว์ปีก", Breed="โรดไอแลนด์เรด", Gender="เมีย", BirthDate=DateTime.Parse("2023-09-01"), Weight=2.10m, HealthStatus="สมบูรณ์ดี", PenNumber="โรงเรือน E1", EstimatedValue=18000m, ImageUrl="https://images.unsplash.com/photo-1548550023-2bdb3c5beed7?w=800&auto=format&fit=crop", Notes="ฝูงไก่ไข่ 60 ตัว อัตราการไข่ 92% ต่อวัน" },
                new Livestock { Code="CHICK-002", Name="ฝูงไก่ชนมงคล", Category="สัตว์ปีก", Breed="ประดู่หางดำ", Gender="ผู้", BirthDate=DateTime.Parse("2023-07-11"), Weight=3.20m, HealthStatus="สมบูรณ์ดี", PenNumber="โรงเรือน E2", EstimatedValue=25000m, ImageUrl="https://images.unsplash.com/photo-1612170153139-6f881ff067e0?w=800&auto=format&fit=crop", Notes="ไก่ไทยสายพันธุ์แท้ โครงสร้างสวยงาม" },
                new Livestock { Code="DUCK-001", Name="ฝูงเป็ดไข่ B", Category="สัตว์ปีก", Breed="กบินทร์บุรี", Gender="เมีย", BirthDate=DateTime.Parse("2023-08-15"), Weight=1.80m, HealthStatus="สมบูรณ์ดี", PenNumber="โรงเรือน E3", EstimatedValue=14000m, ImageUrl="https://images.unsplash.com/photo-1516139008210-96e45dccd83b?w=800&auto=format&fit=crop", Notes="ฝูงเป็ดไข่ 50 ตัว ให้ไข่ฟองใหญ่" },
                new Livestock { Code="DUCK-002", Name="ฝูงเป็ดเทศ C", Category="สัตว์ปีก", Breed="บาบารี่", Gender="ผู้", BirthDate=DateTime.Parse("2023-10-02"), Weight=3.50m, HealthStatus="สมบูรณ์ดี", PenNumber="โรงเรือน E4", EstimatedValue=8000m, ImageUrl="https://images.unsplash.com/photo-1555854877-bab0e564b8d5?w=800&auto=format&fit=crop", Notes="เลี้ยงง่าย เจริญเติบโตไว" }
            };

            foreach (var s in livestocks)
            {
                context.Livestocks.Add(s);
            }
            context.SaveChanges();
        }
    }
}