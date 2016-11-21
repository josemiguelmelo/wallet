using System;
using System.Collections.Generic;

namespace wallet
{
	public class Wallet
	{
		private Dictionary<String, float> wallet;

		public Wallet()
		{
			this.wallet = new Dictionary<string, float>();
		}

		public void addAmount(float amount, String currency)
		{
			if (! this.wallet.ContainsKey(currency))
				this.wallet.Add(currency, amount);
			else
				this.wallet[currency] += amount;
		}

		public float totalAmount(String currency)
		{
			float totalWallet = 0;

			foreach(KeyValuePair<string, float> entry in this.wallet)
			{
				totalWallet = 0;
			}

			return totalWallet;
		}
	}
}
