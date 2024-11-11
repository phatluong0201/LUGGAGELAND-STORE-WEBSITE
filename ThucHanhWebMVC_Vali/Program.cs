using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ThucHanhWebMVC_Vali.Models.Authentication;
using ThucHanhWebMVC_Vali.Models;
using ThucHanhWebMVC_Vali.Repository;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Thời gian hết hạn session
    options.Cookie.IsEssential = true;  // Đảm bảo cookie là cần thiết
});

// Thêm DbContext
builder.Services.AddDbContext<QlbanVaLiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QlbanVaLiContext")));

// Thêm các dịch vụ khác
builder.Services.AddScoped<I_LoaiSpRepository, LoaiSpRepository>();

// Thêm controllers với views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Sử dụng session middleware
app.UseSession();

// Cấu hình HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Đảm bảo session được sử dụng trước khi routing
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");  // Mặc định trang login nếu chưa đăng nhập

app.Run();
