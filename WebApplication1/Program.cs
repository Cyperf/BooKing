using Microsoft.AspNetCore.Builder;

WebApplication1.SQL.AdoNet.Init(); 
WebApplication1.SQL.AdoNet.ExecuteNonQuery("\r\nCREATE TABLE Skole(\r\n\tId int IDENTITY(1,1) NOT NULL PRIMARY KEY,\r\n\tLokation varchar(30)\r\n);\r\n\r\nCREATE TABLE BrugerRolle(\r\n\tId int IDENTITY(1,1) NOT NULL PRIMARY KEY,\r\n\tRolleNavn varchar(30),\r\n\tDagesVarselIndenOverskrivelse int NULL,\r\n);\r\n\r\nCREATE TABLE Bruger(\r\n\tName varchar(20) NOT NULL,\r\n\tEmail varchar(30) NOT NULL PRIMARY KEY,\r\n\tKode varchar(32) NOT NULL,\r\n\tRolle int NOT NULL FOREIGN KEY REFERENCES BrugerRoller(Id),\r\n\tSkoleId int NOT NULL FOREIGN KEY REFERENCES Skole(Id),\r\n\tSletningsDato date NULL\r\n);\r\n\r\nCREATE TABLE Lokale(\r\n\tId int NOT NULL,\r\n\tSkoleId int NOT NULL FOREIGN KEY REFERENCES Skole(Id),\r\n\tHarSmartboard bool NOT NULL,\r\n\tPRIMARY KEY (Id, SkoleId)\r\n);\r\n\r\nCREATE TABLE BookingType(\r\n\tId int IDENTITY(1,1) NOT NULL PRIMARY KEY,\r\n\tTypeNavn varchar(10) NOT NULL\r\n);\r\n\r\nCREATE TABLE Booking(\r\n\tId int IDENTITY(1,1) NOT NULL PRIMARY KEY,\r\n\tDato date NOT NULL,\r\n\tTidFra int NOT NULL,\r\n\tTidTil int NOT NULL,\r\n\tBrugerEmail varchar(30) NOT NULL FOREIGN KEY REFERENCES Bruger(Email),\r\n\tLokaleId int NOT NULL FOREIGN KEY REFERENCES Lokale(Id),\r\n\tSkoleId int NOT NULL FOREIGN KEY REFERENCES Lokale(SkoleId),\r\n\tType int NOT NULL FOREIGN KEY REFERENCES BookingType(Id)\r\n);\r\n");
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
