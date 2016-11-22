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
		public List<Currency> currencies;
		public Wallet myWallet;

		public WalletChartView walletChart;
		public TotalAmountView totalAmountView;

		public WalletPage()
		{	
			currencies = Currency.LoadCurrencies();
			myWallet = new Wallet();

			myWallet.addAmount(10.2f, currencies.Find(x => x.Code == "EUR"));
			myWallet.addAmount(20f,  currencies.Find(x => x.Code == "USD"));
			myWallet.addAmount(25f,  currencies.Find(x => x.Code == "GBP"));
			myWallet.addAmount(52f, currencies.Find(x => x.Code == "BRL"));

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

			Content = layout;
		}

		public void WalletUpdated()
		{
			this.walletChart.UpdateChart();
			this.totalAmountView.UpdateTotalAmount();
		}

	}
}
