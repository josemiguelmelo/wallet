using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace wallet
{
	public class TotalAmountView : StackLayout
	{
		Wallet myWallet;
		List<Currency> currencies;

		Label totalAmountText;
		Label pickerLabelText;
		public CurrencyPicker totalCurrencyPicker;

		public TotalAmountView(Wallet myWallet, List<Currency> currencies)
		{
			this.myWallet = myWallet;
			this.currencies = currencies;

			this.configView();

			this.initTotalAmountTextElement();

			this.initCurrencyPicker();

			StackLayout layout = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.Center
			};

			// Add total amount text element to view
			this.Children.Add(this.totalAmountText);

			// Add picker label and currency picker to layout
			layout.Children.Add(this.pickerLabelText);
			layout.Children.Add(this.totalCurrencyPicker);

			// Add layout to main view
			this.Children.Add(layout);
		}

		private void configView()
		{
			Orientation = StackOrientation.Vertical;
			VerticalOptions = LayoutOptions.End;
			HorizontalOptions = LayoutOptions.Center;
			Padding = new Thickness(0, Device.OnPlatform(20, 20, 20), 0, 10);
		}


		private void initTotalAmountTextElement()
		{
			this.totalAmountText = new Label
			{
				Text = "Total Amount: " + 
						this.myWallet.totalAmount(Currency.DefaultCurrency).ToString("n2") + " " + Currency.DefaultCurrency,
                Font = Font.BoldSystemFontOfSize(14),
                HorizontalOptions = LayoutOptions.Center
            }; 
		}

		private void initCurrencyPicker()
		{
			this.pickerLabelText = new Label
            {
                Text = "Total Amount Currency",
                FontSize = Device.GetNamedSize (NamedSize.Small, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

			this.totalCurrencyPicker = new CurrencyPicker(this.currencies);
			// Set selected index to default currency
			this.totalCurrencyPicker.SelectedIndex = currencies.FindIndex(x => x.Code == Currency.DefaultCurrency);
			// Add event handler
			this.totalCurrencyPicker.SelectedIndexChanged += (sender, args) =>
                {
                    if (totalCurrencyPicker.SelectedIndex == -1)
                    {
						totalAmountText.Text = "Total Amount: " + 
									myWallet.totalAmount(Currency.DefaultCurrency).ToString("n2") + " " + Currency.DefaultCurrency;
                    }
                    else
                    {
						this.UpdateTotalAmount();
                    }
                };
		}

		public void UpdateTotalAmount() { 
			string selectedCode = totalCurrencyPicker.getSelectedCurrencyCode();
            totalAmountText.Text = "Total Amount: " + 
						myWallet.totalAmount(selectedCode).ToString("n2") + " " + selectedCode;
		}
	}
}
