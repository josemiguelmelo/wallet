using System;
using System.Collections.Generic;
using System.Globalization;
using SQLite;

namespace wallet
{
	public class Rate
	{
		[PrimaryKey, AutoIncrement]
		public long ID { get; set;}

		public string FromCurrency { get; set; }
		public string ToCurrency { get; set; }
		public float Value { get; set; }

		public Rate() { 
		}
		public Rate(string FromCurrency, string ToCurrency, float Value)
		{
			this.FromCurrency = FromCurrency;
			this.ToCurrency = ToCurrency;
			this.Value = Value;
		}

	}
}
