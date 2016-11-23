using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace wallet
{
	public class Wallet
	{
		
		Database db;

		private Dictionary<Currency, float> wallet;

		public List<Rate> rates;

		public Wallet(List<Rate> rates)
		{
			this.db = new Database();
			this.wallet = new Dictionary<Currency, float>();
			this.rates = rates;
		}

		public void addAmount(float amount, Currency currency)
		{
			if (! this.wallet.ContainsKey(currency))
				this.wallet.Add(currency, amount);
			else
				this.wallet[currency] += amount;

			UpdateWalletOnDB();
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

			UpdateWalletOnDB();
		}

		public float totalAmount(String currency)
		{
			float totalWallet = 0;

			foreach(KeyValuePair<Currency, float> entry in this.wallet)
			{
				float rate = Currency.findRate(entry.Key.Code, currency, this.rates);
				if (rate != -1)
					totalWallet += (rate * entry.Value);
			}

			db.UpdateRateList(this.rates);

			return totalWallet;
		}

		public Dictionary<Currency, float> getWallet()
		{
			return this.wallet;
		}

		public void LoadAmountsFromWalletDbModel(List<WalletDbModel> list, List<Currency> currencies) {
			foreach (WalletDbModel model in list)
			{
				this.addAmount(model.Amount, currencies.Find(x => x.Code == model.Currency));
			}
		}

		public void UpdateWalletOnDB() {
			List<WalletDbModel> walletList = new List<WalletDbModel>();
			foreach(KeyValuePair<Currency, float> entry in this.wallet)
			{
				WalletDbModel m = new WalletDbModel();
				m.Amount = entry.Value;
				m.Currency = entry.Key.Code;
				walletList.Add(m);
			}
			
			db.UpdateWalletDbModelList(walletList);
		}
	}
}
