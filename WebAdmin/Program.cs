using ClassLibrary1.DTOModels;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebAdmin;
using WebAdmin.DTOModels.Response;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddWebApplicationServices();

builder.Services.AddRazorPages();

#region database
builder.Services.AddDbContext<KOKDatabaseContext>(options =>
{

    options.UseSqlServer("name=ConnectionStrings:database");
});
#endregion 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
