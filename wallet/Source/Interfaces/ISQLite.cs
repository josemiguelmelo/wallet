using System;
using SQLite;

namespace wallet
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}
