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

		public WalletPage()
		{	
			currencies = Currency.LoadCurrencies();
			myWallet = new Wallet();

			myWallet.addAmount(10.2f, new Currency("Euro", "EUR"));
			myWallet.addAmount(20f,  new Currency("Dollar", "USD"));
			myWallet.addAmount(25f,  new Currency("Pound", "GBP"));
			myWallet.addAmount(52f,  new Currency("Real", "BRL"));

			StackLayout layout = new StackLayout
			{
				Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),
    			VerticalOptions = LayoutOptions.FillAndExpand,
    			HorizontalOptions = LayoutOptions.FillAndExpand
			};

			// TODO: find device height and make chart height to 50% 
			int walletChartHeight = 250;

			layout.Children.Add(new WalletChartView(myWallet, walletChartHeight));

			layout.Children.Add(new TotalAmountView(this.myWallet, this.currencies));

			Content = layout;
		}


	}
}
