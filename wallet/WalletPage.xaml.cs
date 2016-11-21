using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
		}
		
	}
}
