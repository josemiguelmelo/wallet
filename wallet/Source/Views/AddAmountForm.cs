using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace wallet
{
	public class AddAmountForm : StackLayout
	{
		WalletPage walletPage;
		
		Wallet myWallet;
		List<Currency> currencies;

		CurrencyPicker currencyPicker;
		Entry amountEntry;
		Button addButton;

		public AddAmountForm(Wallet myWallet, List<Currency> currencies, WalletPage walletPage)
		{
			this.myWallet = myWallet;
			this.currencies = currencies;
			this.walletPage = walletPage;

			initCurrencyPicker();
			initAddButton();

			
			Orientation = StackOrientation.Horizontal;
			VerticalOptions = LayoutOptions.End;
			HorizontalOptions = LayoutOptions.Start;

			this.Children.Add(amountEntry);
			this.Children.Add(currencyPicker);
			this.Children.Add(addButton);
		}

		private void initAddButton()
		{
			this.addButton = new Button
            {
                Text = "Add",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            this.addButton.Clicked += OnButtonClicked;

		}

		void OnButtonClicked(object sender, EventArgs e)
        {
			string selectedCode = this.currencyPicker.getSelectedCurrencyCode();

			string amountString = this.amountEntry.Text;
			float amount = float.Parse(amountString, CultureInfo.InvariantCulture.NumberFormat);

			myWallet.addAmount(amount, currencies[this.currencyPicker.SelectedIndex]);

			this.walletPage.WalletUpdated();
        }

		private void initCurrencyPicker()
		{
			this.amountEntry = new Entry
            {
                Placeholder = "Amount",
                FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label))
            };

			this.currencyPicker = new CurrencyPicker(this.currencies);
			// Set selected index to default currency
			this.currencyPicker.SelectedIndex = currencies.FindIndex(x => x.Code == Currency.DefaultCurrency);
		}
	}
}
