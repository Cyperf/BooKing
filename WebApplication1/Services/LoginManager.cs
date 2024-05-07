namespace WebApplication1.Services
{
	using WebApplication1.Models;
	using WebApplication1.SQL;
	public class LoginManager
	{
		public static WebApplication1.Models.Bruger? LoggedInUser { get; private set; } = null;
		public static bool Login(string email, string password)
		{
			string hashedPassword = HashPassword(email, password);
			bool b = false;
			AdoNet.ExecuteQuery($"SELECT Name, Email, Kode, Rolle, SkoleId, SletningsDato FROM bruger WHERE Email={email} AND Kode={hashedPassword}",
				reader =>
				{
					if (!reader.Read())
						return;
					b = true;
					LoggedInUser = new Bruger(reader.GetString(0), reader.GetString(1), reader.GetString(2), GetUserRoleFromId(reader.GetInt32(3)), reader.GetInt32(4), DateOnly.FromDateTime(reader.GetDateTime(5)));
					});
			return b;
		}

		private static BrugerRolle GetUserRoleFromId(int id)
		{
			BrugerRolle rolle = null;
			AdoNet.ExecuteQuery($"SELECT RolleNavn, DagesVarselIndenOverskrivelse FROM BrugerRolle WHERE Id={id}",
				reader =>
				{
					reader.Read();
					BrugerRolle rolle = new BrugerRolle(reader.GetString(0), reader.IsDBNull(1) ? null : reader.GetInt32(1));
				});
			return rolle;
		}

		public static string HashPassword(string email, string password)
		{
			return new Microsoft.AspNetCore.Identity.PasswordHasher<string>().HashPassword(email, password);
        }
	}
}
