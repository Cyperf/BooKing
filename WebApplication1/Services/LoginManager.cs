namespace WebApplication1.Services
{
	using WebApplication1.Models;
	using WebApplication1.SQL;
	public class LoginManager
	{
		public static WebApplication1.Models.Bruger? LoggedInUser { get; private set; } = null;
		public bool Login(string email, string password)
		{
			bool b = false;
			AdoNet.ExecuteQuery($"SELECT Name, Email, Kode, Rolle, SkoleId, SletningsDato FROM bruger WHERE Email={email} AND Kode={password}",
				reader =>
				{
					if (!reader.Read())
						return;
					b = true;

					});
			return b;
		}
	}
}
