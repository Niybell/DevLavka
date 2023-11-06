using DevLavka.Models;
using DevLavka.Models.EntityFramework;
using DevLavka.Models.EntityFramework.Repositories;
using DevLavka.Services.Implementations;
using DevLavka.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();   
builder.Services.AddDbContext<AuthDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionString")));
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionString")));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AuthDBContext>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IBaseRepository<Article>, ArticlesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
