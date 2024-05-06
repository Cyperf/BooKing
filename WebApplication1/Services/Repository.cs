namespace WebApplication1.Services
{
	using WebApplication1.SQL;
	public abstract class Repository<T>
	{
		protected string _tableName = "";
		protected abstract Func<T, string> _fromItemToString { get; }
		protected abstract Func<Microsoft.Data.SqlClient.SqlDataReader, T> _fromReaderToItem { get; }
		//protected Dictionary<string, Type> _columNamesAndTypes = new Dictionary<string, Type>();
		/// <summary>
		/// Adds an item to the database
		/// </summary>
		/// <param name="newItem"></param>
		public void Create(T newItem)
		{
			AdoNet.ExecuteNonQuery(_fromItemToString(newItem));
		}
		/// <summary>
		/// Reads one item from the database, with a given id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public T? Read(int id)
		{
			T? item = default;
			AdoNet.ExecuteQuery($"SELECT * FROM {_tableName} WHERE id={id}", (reader) =>
			{
				if (!reader.Read())
					return;
				item = _fromReaderToItem(reader);
			});
			return item;
		}
		/// <summary>
		/// Reads all the items from the database, from a given table
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> ReadAll(string where = "")
		{
			List<T> items = new List<T>();
			AdoNet.ExecuteQueryForEach($"SELECT * FROM {_tableName}" + ((!string.IsNullOrEmpty(where)) ? ("WHERE " + where) : ""), (reader) =>
			{
				items.Add(_fromReaderToItem(reader));
			});
			return items;
		}
		public abstract void Update(T item);;
		//{
		//	AdoNet.ExecuteNonQuery($"UPDATE {_tableName} SET");
		//}
		public bool Delete() { throw new NotImplementedException(); }
	}
}
