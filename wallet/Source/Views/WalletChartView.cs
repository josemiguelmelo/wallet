using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace wallet
{
	public class WalletChartView : StackLayout
	{
		public WalletChartView(Wallet myWallet, int height)
		{

			Orientation = StackOrientation.Horizontal;
			VerticalOptions = LayoutOptions.End;
			HorizontalOptions = LayoutOptions.Center;
			HeightRequest = height;
		

			Color[] colors = { Color.Red, Color.Blue, Color.Green};
			var i = 0;

			foreach (KeyValuePair<Currency, float> item in myWallet.getWallet())
			{
				Debug.WriteLine(item.Value);
				StackLayout column = new StackLayout
				{
					VerticalOptions = LayoutOptions.End 
				};

				BoxView view = new BoxView
				{
					Color = colors[i % 3],
					WidthRequest = 20,
					HeightRequest = item.Value,
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
