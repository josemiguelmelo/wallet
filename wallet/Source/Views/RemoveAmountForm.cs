using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace wallet
{
	public class RemoveAmountForm : StackLayout
	{
		WalletPage walletPage;
		
		Wallet myWallet;
		List<Currency> currencies;

		CurrencyPicker currencyPicker;
		Entry amountEntry;
		Button addButton;

		public RemoveAmountForm(Wallet myWallet, List<Currency> currencies, WalletPage walletPage)
		{
			this.myWallet = myWallet;
			this.currencies = currencies;
			this.walletPage = walletPage;

			initCurrencyPicker();
			initAddButton();

			
			Orientation = StackOrientation.Horizontal;
			VerticalOptions = LayoutOptions.End;
			HorizontalOptions = LayoutOptions.Center;
			Padding = new Thickness(0, Device.OnPlatform(10, 10, 10), 0, 0);

			this.Children.Add(amountEntry);
			this.Children.Add(currencyPicker);
			this.Children.Add(addButton);
		}

		private void initAddButton()
		{
			this.addButton = new Button
            {
                Text = "Remove",
                Font = Font.SystemFontOfSize(NamedSize.Small),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
				WidthRequest = 70
            };
            this.addButton.Clicked += OnButtonClicked;

		}

		void OnButtonClicked(object sender, EventArgs e)
        {
			string selectedCode = this.currencyPicker.getSelectedCurrencyCode();

			string amountString = this.amountEntry.Text;
			float amount = float.Parse(amountString, CultureInfo.InvariantCulture.NumberFormat);

			myWallet.removeAmount(amount, currencies[this.currencyPicker.SelectedIndex]);

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
