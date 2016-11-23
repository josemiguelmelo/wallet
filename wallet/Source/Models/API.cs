using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace wallet
{
	public class API
	{
		HttpClient client;

		public API()
		{
			this.client = new HttpClient();
		}

		public bool isInternetAvailable() { 
			Uri uriQuotes = new Uri (string.Format ("http://www.google.com", string.Empty));

			try
			{
				var response = client.GetAsync(uriQuotes).Result;
				if (response.IsSuccessStatusCode) {
					return true;
				}
				return false;
			}
			catch (Exception e){
				return false;
			}
			
		}
		public async Task<string> GetRate(string fromCurrency, string toCurrency)
        {
			Uri uriQuotes = new Uri (string.Format ("http://download.finance.yahoo.com/d/quotes?f=sl1d1t1&s="+fromCurrency+toCurrency+"=X", string.Empty));
			
			var response = await client.GetAsync (uriQuotes);
			if (response.IsSuccessStatusCode) {
				var content = await response.Content.ReadAsStringAsync ();
				Debug.WriteLine(content);
				return content;
			}
			return "";
        }

		public async Task<List<Rate>> DownloadMainConversionRates(List<Currency> currencies)
        {
			List<Rate> rates = new List<Rate>();

			List<Currency> mainCurrencies = currencies.FindAll(x =>
						(x.Code == Currency.DefaultCurrency
						|| x.Code == "USD"
						|| x.Code == "GBP"
						|| x.Code == "BRL"));
			
			foreach (Currency fromCurrency in mainCurrencies) { 
				foreach (Currency toCurrency in mainCurrencies) {
					if (fromCurrency.Code == toCurrency.Code)
						continue;
					string rateResultString = await GetRate(fromCurrency.Code, toCurrency.Code);

					char[] delimiterChar = { ',' };
					string[] responseParts = rateResultString.Split(delimiterChar);

					float rateFloat;
					if (float.TryParse(responseParts[1], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture.NumberFormat, out rateFloat))
					{
						rates.Add(new Rate(fromCurrency.Code, toCurrency.Code, rateFloat));
					}
				}
			}
			return rates;
        }


		
	}
}
