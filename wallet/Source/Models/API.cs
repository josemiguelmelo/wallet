using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace wallet
{
	public class API
	{
		HttpClient client;

		public API()
		{
			this.client = new HttpClient();
		}

		public async Task<string> DownloadRates(string fromCurrency, string toCurrency)
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

		
	}
}
