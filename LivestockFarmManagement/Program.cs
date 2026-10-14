using Microsoft.EntityFrameworkCore;
using LivestockFarmManagement.Data;

var builder = WebApplication.CreateBuilder(args);

// ตั้งค่า DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔄 อัปเดตโครงสร้างตารางเดิมให้อัตโนมัติ (เพิ่มคอลัมน์ Notes, CategoryName ฯลฯ โดยไม่ลบข้อมูลสัตว์เดิม)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // 1. ตรวจสอบและสร้างฐานข้อมูลหากยังไม่มี
    context.Database.EnsureCreated();

    // 2. สั่งสร้างตาราง UserAccounts หากยังไม่มีในฐานข้อมูลเดิม
    context.Database.ExecuteSqlRaw(@"
        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'UserAccounts')
        BEGIN
            CREATE TABLE UserAccounts (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Username NVARCHAR(MAX) NOT NULL,
                Password NVARCHAR(MAX) NOT NULL,
                FullName NVARCHAR(MAX) NULL,
                Role NVARCHAR(MAX) NOT NULL DEFAULT 'Admin',
                IsActive BIT NOT NULL DEFAULT 1
            );
        END
    ");

    // 3. สั่งสร้างตาราง SpeciesBreeds หากยังไม่มีในฐานข้อมูลเดิม
    context.Database.ExecuteSqlRaw(@"
        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SpeciesBreeds')
        BEGIN
            CREATE TABLE SpeciesBreeds (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Category NVARCHAR(MAX) NOT NULL,
                BreedName NVARCHAR(MAX) NOT NULL,
                GestationDays INT NOT NULL DEFAULT 283
            );
        END
    ");

    // 4. สั่งเพิ่มคอลัมน์ที่ขาดในตาราง FinancialTransactions (แก้ปัญหา Invalid column name 'Notes')
    context.Database.ExecuteSqlRaw(@"
        IF EXISTS (SELECT * FROM sys.tables WHERE name = 'FinancialTransactions')
        BEGIN
            IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FinancialTransactions') AND name = 'Notes')
            BEGIN
                ALTER TABLE FinancialTransactions ADD Notes NVARCHAR(MAX) NULL;
            END

            IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FinancialTransactions') AND name = 'CategoryName')
            BEGIN
                ALTER TABLE FinancialTransactions ADD CategoryName NVARCHAR(MAX) NULL;
            END

            IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('FinancialTransactions') AND name = 'TransactionType')
            BEGIN
                ALTER TABLE FinancialTransactions ADD TransactionType NVARCHAR(MAX) NULL;
            END
        END
    ");

    // 🔒 ปิด Seed Data ไว้ เพื่อรักษาข้อมูลและรูปภาพสัตว์เลี้ยงเดิมไม่ให้โดนรีเซ็ตทับ
    // DbInitializer.Initialize(context);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// ตั้งค่า Route เริ่มต้นไปหน้า Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();