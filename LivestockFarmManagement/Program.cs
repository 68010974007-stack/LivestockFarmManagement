using Microsoft.EntityFrameworkCore;
using LivestockFarmManagement.Data;

var builder = WebApplication.CreateBuilder(args);

// ตั้งค่า DbContext ให้ใช้ SQLite (รองรับการ deploy บน Linux Container / Render)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// 🔄 อัปเดตโครงสร้างตารางเดิมให้อัตโนมัติสำหรับ SQLite
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();

    // 1. ตรวจสอบและสร้างฐานข้อมูลพร้อมตารางทั้งหมดตาม Models หากยังไม่มี
    context.Database.EnsureCreated();

    // 2. สั่งสร้างตาราง UserAccounts หากยังไม่มีในฐานข้อมูล
    context.Database.ExecuteSqlRaw(@"
        CREATE TABLE IF NOT EXISTS UserAccounts (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Username TEXT NOT NULL,
            Password TEXT NOT NULL,
            FullName TEXT NULL,
            Role TEXT NOT NULL DEFAULT 'Admin',
            IsActive INTEGER NOT NULL DEFAULT 1
        );
    ");

    // 3. สั่งสร้างตาราง SpeciesBreeds หากยังไม่มีในฐานข้อมูล
    context.Database.ExecuteSqlRaw(@"
        CREATE TABLE IF NOT EXISTS SpeciesBreeds (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Category TEXT NOT NULL,
            BreedName TEXT NOT NULL,
            GestationDays INTEGER NOT NULL DEFAULT 283
        );
    ");

    // 4. สั่งเพิ่มคอลัมน์ที่ขาดในตาราง FinancialTransactions (ปลอดภัยสำหรับ SQLite)
    try
    {
        context.Database.ExecuteSqlRaw("ALTER TABLE FinancialTransactions ADD COLUMN Notes TEXT;");
    }
    catch { /* ข้ามหากคอลัมน์มีอยู่แล้ว หรือตารางยังไม่ได้สร้าง */ }

    try
    {
        context.Database.ExecuteSqlRaw("ALTER TABLE FinancialTransactions ADD COLUMN CategoryName TEXT;");
    }
    catch { /* ข้ามหากคอลัมน์มีอยู่แล้ว หรือตารางยังไม่ได้สร้าง */ }

    try
    {
        context.Database.ExecuteSqlRaw("ALTER TABLE FinancialTransactions ADD COLUMN TransactionType TEXT;");
    }
    catch { /* ข้ามหากคอลัมน์มีอยู่แล้ว หรือตารางยังไม่ได้สร้าง */ }
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