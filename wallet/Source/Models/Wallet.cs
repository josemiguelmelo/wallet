using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace wallet
{
	public class Wallet
	{
		private Dictionary<Currency, float> wallet;

		public Wallet()
		{
			this.wallet = new Dictionary<Currency, float>();
		}

		public void addAmount(float amount, Currency currency)
		{
			if (! this.wallet.ContainsKey(currency))
				this.wallet.Add(currency, amount);
			else
				this.wallet[currency] += amount;
		}

		public void removeAmount(float amount, Currency currency)
		{
			if (!this.wallet.ContainsKey(currency))
				return;
			else {
				this.wallet[currency] -= amount;
				if (this.wallet[currency] <= 0)
					this.wallet.Remove(currency);
			}
		}

		public float totalAmount(String currency)
		{
			float totalWallet = 0;

			foreach(KeyValuePair<Currency, float> entry in this.wallet)
			{
				var rate = Currency.findRate(entry.Key.Code, currency);
				totalWallet += (rate * entry.Value);
			}
			Debug.WriteLine(totalWallet);
			return totalWallet;
		}

		public Dictionary<Currency, float> getWallet()
		{
			return this.wallet;
		}

		
	}
}
