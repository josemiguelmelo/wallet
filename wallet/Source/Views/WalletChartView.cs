using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace wallet
{
	public class WalletChartView : StackLayout
	{
		Wallet myWallet;
		WalletPage parent;

		public WalletChartView(Wallet myWallet, int height, WalletPage parent)
		{
			this.myWallet = myWallet;
			this.parent = parent;

			Orientation = StackOrientation.Horizontal;
			VerticalOptions = LayoutOptions.End;
			HorizontalOptions = LayoutOptions.Center;
			HeightRequest = height;

			this.CreateChart();
		}

		public void UpdateChart()
		{
			this.Children.Clear();
			this.CreateChart();
		}

		private void CreateChart()
		{
			Color[] colors = { Color.Red, Color.Blue, Color.Green};
			var i = 0;

			foreach (KeyValuePair<Currency, float> item in this.myWallet.getWallet())
			{
				Debug.WriteLine(item.Value);
				StackLayout column = new StackLayout
				{
					VerticalOptions = LayoutOptions.End 
				};

				float rate = Currency.findRate(item.Key.Code,
						this.parent.totalAmountView.totalCurrencyPicker.getSelectedCurrencyCode(),
						this.parent.rates);

				BoxView view = new BoxView
				{
					Color = colors[i % 3],
					WidthRequest = 20,
					HeightRequest = item.Value * rate,
					HorizontalOptions = LayoutOptions.Start,
					VerticalOptions = LayoutOptions.End
				};
				Label description = new Label
	            {
	                Text = item.Value.ToString("n2") + " " + item.Key.Code,
	                Font = Font.BoldSystemFontOfSize(12),
	                HorizontalOptions = LayoutOptions.Center
	            }; 

				column.Children.Add(view);
				column.Children.Add(description);

				this.Children.Add(column);
				i++;
			}
		}
	}
}
