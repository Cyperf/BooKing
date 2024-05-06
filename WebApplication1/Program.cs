using Microsoft.AspNetCore.Builder;
using System.Diagnostics;

WebApplication1.SQL.AdoNet.Init();
//Debug.WriteLine("PASSWORD LENGTH: \n" + new Microsoft.AspNetCore.Identity.PasswordHasher<string>().HashPassword("user", "password").Length);
//WebApplication1.SQL.AdoNet.ExecuteNonQuery("INSERT INTO Skole VALUES ('Roskilde')");
//WebApplication1.SQL.AdoNet.ExecuteNonQuery("INSERT INTO Skole VALUES ('Sorø')");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



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
