using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace wallet
{
	public class WalletPage : ContentPage
	{
		public Database db;
		public List<Rate> rates;
		public List<Currency> currencies;
		public Wallet myWallet;

		public WalletChartView walletChart;
		public TotalAmountView totalAmountView;

		public API api;

		public WalletPage()
		{
			this.api = new API();
			this.db = new Database();

			this.rates = db.GetRates();
			List<WalletDbModel> walletAmountsList = this.db.GetWalletDbModel();
			currencies = Currency.LoadCurrencies();

			this.LoadRates();

			myWallet = new Wallet(this.rates);
			myWallet.LoadAmountsFromWalletDbModel(walletAmountsList, this.currencies);


			StackLayout layout = new StackLayout
			{
				Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
    			VerticalOptions = LayoutOptions.FillAndExpand,
    			HorizontalOptions = LayoutOptions.FillAndExpand
			};

			// TODO: find device height and make chart height to 50% 
			int walletChartHeight = 250;

			this.walletChart = new WalletChartView(myWallet, walletChartHeight);
			this.totalAmountView = new TotalAmountView(this.myWallet, this.currencies);

			layout.Children.Add(this.walletChart);

			layout.Children.Add(this.totalAmountView);

			layout.Children.Add(new AddAmountForm(this.myWallet, this.currencies, this));
			layout.Children.Add(new RemoveAmountForm(this.myWallet, this.currencies, this));

			Content = layout;
		}

		public void LoadRates()
		{
			Task.Run(async () => {
				if (api.isInternetAvailable())
				{
					Debug.WriteLine("Internet available.");

					await DownloadRatesAndStore();
				}
				else {
					Debug.WriteLine("Internet not available. ");
					// TODO: ADD ALERT TO CONNECT TO INTERNET FOR FIRST TIME
				}
			});
		}


		private async Task DownloadRatesAndStore()
		{
			List<Rate> ratesDownloaded = await api.DownloadMainConversionRates(this.currencies);

			foreach (Rate rate in ratesDownloaded)
			{
				int index = this.rates.FindIndex(x => (x.FromCurrency == rate.FromCurrency && x.ToCurrency == rate.ToCurrency));
				if (index != -1)
					this.rates[index].Value = rate.Value;
				else
					this.rates.Add(rate);
			}

			try
			{
				db.AddRateList(this.rates);
			}
			catch (Exception e) {
				Debug.WriteLine(e); 
			}

			NotifyDataChanged();
		}

		public void WalletUpdated()
		{
			this.walletChart.UpdateChart();
			this.totalAmountView.UpdateTotalAmount();
		}

		public void NotifyDataChanged()
		{
			myWallet.rates = this.rates;
			WalletUpdated();
		}

	}
}
