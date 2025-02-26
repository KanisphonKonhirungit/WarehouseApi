using Microsoft.EntityFrameworkCore;
using WarehouseApi.Models;

var builder = WebApplication.CreateBuilder(args);

// กำหนด Connection String จากไฟล์ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// เพิ่มบริการ DbContext ที่จะใช้ในการติดต่อกับ MSSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// เพิ่มบริการที่จำเป็นสำหรับ Web API
builder.Services.AddControllers();

// เพิ่มการตั้งค่า Swagger UI สำหรับ API Documentation
builder.Services.AddSwaggerGen();  // เพิ่มการตั้งค่านี้

// สร้างแอปพลิเคชัน
var app = builder.Build();

// ใช้ Swagger UI ถ้าเป็นการพัฒนา
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // เพิ่มการใช้งาน Swagger
    app.UseSwaggerUI();  // เพิ่มการใช้งาน Swagger UI
}

// ใช้การ Authentication และ Authorization ถ้าต้องการ (สามารถเพิ่มได้ในอนาคต)
app.UseAuthorization();

// ระบุว่าใช้ Controller สำหรับ Routing
app.MapControllers();

// สตาร์ทแอป
app.Run();
