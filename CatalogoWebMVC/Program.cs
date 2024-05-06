using CatalogoWebMVC.Data;
using CatalogoWebMVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Creamos la politica de seguridad, solo los usuarios autenticados puede nav por la app

string strConexion = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

// Aplicacmos la politica de seguridad
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opciones =>
{
    opciones.SignIn.RequireConfirmedAccount = false;
}).AddEntityFrameworkStores<CATALOGO_WEB_DBContext>()
.AddDefaultTokenProviders();

builder.Services.AddDbContext<CATALOGO_WEB_DBContext>( options => options.UseSqlServer(strConexion));
builder.Services.AddScoped<IRepositorioArticulos, RepositorioArticulos>();
builder.Services.AddScoped<IRepositorioMarcas, RepositorioMarcas>();
builder.Services.AddScoped<IRepositorioCategorias, RepositorioCategorias>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.PostConfigure <CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
    opciones =>
    {
        opciones.LoginPath = "/usuarios/login";
        opciones.AccessDeniedPath = "/usuarios/login";
    });

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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Articulo}/{action=Index}/{id?}");

app.Run();
