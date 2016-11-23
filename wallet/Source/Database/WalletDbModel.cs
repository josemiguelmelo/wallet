using System;
using SQLite;

namespace wallet
{
	public class WalletDbModel
	{
		[PrimaryKey, AutoIncrement]
		public long ID { get; set;}

		public string Currency { get; set; }
		public float Amount { get; set; }
		public WalletDbModel()
		{
		}

	}
}
