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
            bool b = false;

			AdoNet.ExecuteQuery($"SELECT Name, Email, Kode, Rolle, SkoleId, SletningsDato FROM bruger WHERE Email='{email}'",
				reader =>
				{
                    // since the query is not case-sensitive, we need to confirm we got the right email, by compairing it to the one put into the function
                    // for example if the input of the query is 'Abc@gmail.com', and the actual email is 'abc@gmail.com', it would still select it
                    bool foundEmail = false;
					while (foundEmail = reader.Read())
					{
						if (reader.GetString(1) == email)
                        {
                            foundEmail = true;
                            break;
                        }
                    }
                    if (!foundEmail)
						return;
					var result = hasher.VerifyHashedPassword(email, reader.GetString(2), password);
                    if (result != Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
						return;
					b = true;
					LoggedInUser = new BrugerService().Read(email);
					//LoggedInUser = new Bruger(reader.GetString(0), reader.GetString(1), reader.GetString(2), GetUserRoleFromId(reader.GetInt32(3)), reader.GetInt32(4), DateOnly.FromDateTime(reader.GetDateTime(5)));
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
