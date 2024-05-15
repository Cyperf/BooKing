using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using WebApplication1.Services;

WebApplication1.SQL.AdoNet.Init();
//new BookingTypeRepository().Create(new WebApplication1.Models.BookingType("Lokale"));
//new BookingTypeRepository().Create(new WebApplication1.Models.BookingType("smartboard"));
//new BrugerService().Create(new WebApplication1.Models.Bruger("bruger", "bruger@gmail.com", "bruger", 2, 1, new DateOnly(2025, 12, 24)));
//LoginManager.Login("abc@gmail.com", "123abc");
//Debug.WriteLine("PASSWORD LENGTH: \n" + new Microsoft.AspNetCore.Identity.PasswordHasher<string>().HashPassword("user", "password").Length);
//WebApplication1.SQL.AdoNet.ExecuteNonQuery("INSERT INTO Skole VALUES ('Roskilde')");
//WebApplication1.SQL.AdoNet.ExecuteNonQuery("INSERT INTO Skole VALUES ('Sorø')");
//new SkoleService().Create(new WebApplication1.Models.Skole(0, "Næstved"));
//new LokaleService().Create(new WebApplication1.Models.Lokale(102, 1, 2, false));
//new LokaleService().Create(new WebApplication1.Models.Lokale(103, 1, 2, true));
//foreach (var a in new LokaleService().ReadAll())
//    System.Diagnostics.Debug.WriteLine(a);
//new BrugerRolleService().Create(new WebApplication1.Models.BrugerRolle("Studerende", null));
//System.Diagnostics.Debug.WriteLine(new BrugerRolleService().Read(1));
//new BrugerService().Create(new WebApplication1.Models.Bruger("aaa", "aaa@gmail.com", "123abc", new BrugerRolleService().Read(1), 1, new DateOnly(2025, 12, 24)));
//System.Diagnostics.Debug.WriteLine(new BrugerService().Read("aaa@gmail.com"));

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
