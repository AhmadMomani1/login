using logintest.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();  // مهم: لتفعيل API controllers

// تفعيل CORS للسماح للفرونت اند (React) بالوصول للـ API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:3000")  // رابط React الافتراضي
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowReactApp");  // تفعيل استخدام CORS

app.UseAuthorization();

app.MapControllers();  // ملاحظة: لا تستخدم MapControllerRoute مع API فقط، استخدم MapControllers

app.Run();
