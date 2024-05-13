namespace WebApplication1.SQL
{
	using Microsoft.Data.SqlClient;
	using System.Diagnostics;

	public static class AdoNet
    {
		private static string _connectionString;

		/// <summary>
		/// Creates the connection string
		/// </summary>
		public static void Init()
        {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource = "mssql14.unoeuro.com"; // server
            builder.InitialCatalog = "frederiknicolajsen_dk_db_booking";           // database navn
			builder.UserID = "frederiknicolajsen_dk";
			builder.Password = "ADR4EaBy29n3Fem5ztdg";
			builder.TrustServerCertificate = true;
			_connectionString = builder.ConnectionString;
		}
        public static void ExecuteNonQuery(string nonQuery)
        {
			Debug.WriteLine("Non query:\n" + nonQuery);
			try
			{
				using SqlConnection connection = new SqlConnection(_connectionString);
				connection.Open();
				SqlCommand cmd = new SqlCommand(nonQuery, connection);
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
        }
		/// <summary>
		/// executes a query on the database
		/// </summary>
		/// <param name="query">the query</param>
		/// <param name="action">use the data given from the database</param>
		public static void ExecuteQuery(string query, Action<SqlDataReader> action)
		{
			Debug.WriteLine("Query:\n" + query);
			try
			{
				using SqlConnection connection = new SqlConnection(_connectionString);
				connection.Open();
				SqlCommand cmd = new SqlCommand(query, connection);
				SqlDataReader reader = cmd.ExecuteReader();
				action(reader);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("\nERROR\n" + ex.ToString());  
			}
		}
		/// <summary>
		/// executes a query on the database
		/// </summary>
		/// <param name="query">the query</param>
		/// <param name="action">use an action, for one row in the database</param>
		public static void ExecuteQueryForEach(string query, Action<SqlDataReader> action)
		{
			Debug.WriteLine("Query (foreach):\n" + query);
			try
			{
				using SqlConnection connection = new SqlConnection(_connectionString);
				connection.Open();
				SqlCommand cmd = new SqlCommand(query, connection);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					action(reader);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
			}
		}
	}
}
