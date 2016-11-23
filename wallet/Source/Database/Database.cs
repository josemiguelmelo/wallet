using System;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;

namespace wallet
{
	public class Database
	{
		SQLiteConnection database;

		public Database()
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();
    		database.CreateTable<Rate>();
    		database.CreateTable<WalletDbModel>();
		}


		#region Rates DB Table
		public List<Rate> GetRates ()
		{
			return database.Query<Rate>("Select * From [Rate]");
		}

		public void AddRate(Rate rate) {
			long s = database.Insert(rate);
			rate.ID = s;
		}
		public void AddRateList(List<Rate> rateList) {
			foreach (Rate rate in rateList)
				AddRate(rate);
		}


		public void UpdateRateList(List<Rate> rateList) {
			database.DeleteAll<Rate>();
			this.AddRateList(rateList);
		}

		#endregion


		#region Wallet DB Table
		
		public List<WalletDbModel> GetWalletDbModel ()
		{
			return database.Query<WalletDbModel>("Select * From [WalletDbModel]");
		}

		public void AddWalletDbModel(WalletDbModel walletDBModel) {
			long s = database.Insert(walletDBModel);
			walletDBModel.ID = s;
		}
		public void AddWalletDbModelList(List<WalletDbModel> walletList) {
			foreach (WalletDbModel w in walletList)
				AddWalletDbModel(w);
		}


		public void UpdateWalletDbModelList(List<WalletDbModel> walletList) {
			database.DeleteAll<WalletDbModel>();
			this.AddWalletDbModelList(walletList);
		}

		#endregion
	}
}
