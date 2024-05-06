namespace WebApplication1.SQL
{
	using Microsoft.Data.SqlClient;

	public static class AdoNet
    {
		private static string _connectionString;

		public static void Init()
        {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.DataSource = "(localdb)\\MSSQLLocalDB"; // server
			builder.InitialCatalog = "Databasen";			// database navn
			_connectionString = builder.ConnectionString;
		}
        public static void ExecuteNonQuery(string nonQuery)
        {
			try
			{
				using SqlConnection connection = new SqlConnection(_connectionString);
				connection.Open();
				SqlCommand cmd = new SqlCommand(nonQuery, connection);
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{

			}
        }
		/// <summary>
		/// executes a query on the database
		/// </summary>
		/// <param name="query">the query</param>
		/// <param name="action">use the data given from the database</param>
		public static void ExecuteQuery(string query, Action<SqlDataReader> action)
		{
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

			}
		}
		/// <summary>
		/// executes a query on the database
		/// </summary>
		/// <param name="query">the query</param>
		/// <param name="action">use an action, for one row in the database</param>
		public static void ExecuteQueryForEach(string query, Action<SqlDataReader> action)
		{
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

			}
		}
	}
}
