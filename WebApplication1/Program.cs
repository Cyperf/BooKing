using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using WebApplication1.Services;

WebApplication1.SQL.AdoNet.Init();
//Debug.WriteLine("PASSWORD LENGTH: \n" + new Microsoft.AspNetCore.Identity.PasswordHasher<string>().HashPassword("user", "password").Length);
//WebApplication1.SQL.AdoNet.ExecuteNonQuery("INSERT INTO Skole VALUES ('Roskilde')");
//WebApplication1.SQL.AdoNet.ExecuteNonQuery("INSERT INTO Skole VALUES ('Sor�')");
//new SkoleService().Create(new WebApplication1.Models.Skole(0, "N�stved"));
//new LokaleService().Create(new WebApplication1.Models.Lokale(102, 1, 2, false));
//new LokaleService().Create(new WebApplication1.Models.Lokale(103, 1, 2, true));

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//h�rer til login page / Roman
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});


string authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
builder.Services.AddAuthentication(authScheme).AddCookie(options =>
{ 
 options.LoginPath = "/Login/LoginPage";
});

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
