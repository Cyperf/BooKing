namespace WebApplication1.Services
{
    using WebApplication1.Models;
    using WebApplication1.SQL;
	public class LoginManager
	{
		public static WebApplication1.Models.Bruger? LoggedInUser { get; private set; } = null;
		public static bool Login(string email, string password)
		{
			var hasher = new Microsoft.AspNetCore.Identity.PasswordHasher<string>();
            //string hashedPassword = hasher.HashPassword(email, password);

            bool b = false;

			AdoNet.ExecuteQuery($"SELECT Name, Email, Kode, Rolle, SkoleId, SletningsDato FROM bruger WHERE Email='{email}'",
				reader =>
				{
                    if (!reader.Read())
						return;
					var result = hasher.VerifyHashedPassword(email, reader.GetString(2), password);
                    if (result != Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
						return;
					b = true;
					LoggedInUser = new Bruger(reader.GetString(0), reader.GetString(1), reader.GetString(2), GetUserRoleFromId(reader.GetInt32(3)), reader.GetInt32(4), DateOnly.FromDateTime(reader.GetDateTime(5)));
				});
			return b;
		}

		private static BrugerRolle GetUserRoleFromId(int id)
		{
			return new BrugerRolleService().Read(id);
		}

        public static string HashPassword(string email, string password)
		{
			return new Microsoft.AspNetCore.Identity.PasswordHasher<string>().HashPassword(email, password);
        }
	}
}
