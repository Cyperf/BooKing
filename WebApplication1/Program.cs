using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using WebApplication1.Services;
using WebApplication1.SQL;

WebApplication1.SQL.AdoNet.Init();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//hører til login page / Roman
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

string authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
builder.Services.AddAuthentication(authScheme).AddCookie(options =>
{ 
 options.LoginPath = "/OurPages/LogIn";
});
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/OurPages/Booking");
    options.Conventions.AuthorizePage("/OurPages/BookLokale");
    options.Conventions.AuthorizePage("/OurPages/BrugerSide");
    options.Conventions.AuthorizePage("/OurPages/Logout");
    options.Conventions.AuthorizePage("/OurPages/OpretBrugerKonto");
    options.Conventions.AuthorizePage("/OurPages/OpretLokaler");
    options.Conventions.AuthorizePage("/OurPages/RedigerBruger");
    options.Conventions.AuthorizePage("/OurPages/RumTyper");
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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
