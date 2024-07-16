using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsuariosModel, UsuariosModel>();
builder.Services.AddScoped<IProveedoresModel, ProveedoresModel>();
builder.Services.AddScoped<IUsuariosRolesModel, UsuariosRolesModel>();
builder.Services.AddScoped<IRegistroDesechosModel, RegistroDesechosModel>();
builder.Services.AddScoped<IProductosModel, ProductosModel>();
builder.Services.AddScoped<ISaboresPizzaModel, SaboresPizzaModel>();
builder.Services.AddScoped<ILogsModel, LogsModel>();
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
