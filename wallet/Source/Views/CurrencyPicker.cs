using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace wallet
{
	public class CurrencyPicker : Picker
	{
		List<Currency> currencies;
		public CurrencyPicker(List<Currency> currencies)
		{
			this.currencies = currencies;

			Title = "Currency";
			VerticalOptions = LayoutOptions.Start;

			this.addItemsToElement();
			
		}

		private void addItemsToElement()
		{
			foreach (Currency currency in currencies)
			{
				this.Items.Add(currency.Code); 
			}
		}

		public String getSelectedCurrencyCode()
		{
			return currencies[this.SelectedIndex].Code;
		}
	}
}
