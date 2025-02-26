using Microsoft.EntityFrameworkCore;
using WarehouseApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();


app.UseCors("AllowAllOrigins");


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

