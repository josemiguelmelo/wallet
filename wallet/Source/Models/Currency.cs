﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace wallet
{
	public class Currency
	{

		public string Code;
		public string Country;
		public static readonly string DefaultCurrency = "EUR";

		public Currency(String country, String code)
		{
			this.Code = code;
			this.Country = country;
		}

		/**	
		* Load currencies from JSON resource file
		*/
		public static List<Currency> LoadCurrencies()
		{
			List<Currency> currenciesList = new List<Currency>();

			var assembly = typeof(WalletPage).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream("wallet.Resources.currency-list.json");

			using (var reader = new System.IO.StreamReader(stream))
			{
				string json = reader.ReadToEnd();
				
				Dictionary<string, string> currenciesDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

				foreach (KeyValuePair<string, string> currency in currenciesDictionary)
				{
					currenciesList.Add(new Currency(currency.Value, currency.Key));
				}
			}
			return currenciesList;
		}

		public static float findRate(string fromCurrency, string toCurrency)
		{
			API api = new API();

			float rate = 0;

			Task.Run(() => { 
				var rateResult = api.DownloadRates(fromCurrency, toCurrency).Result;
				char[] delimiterChar = { ',' };
				string[] responseParts = rateResult.Split(delimiterChar);
				rate = float.Parse(responseParts[1], CultureInfo.InvariantCulture.NumberFormat);
			}).Wait();

			return rate;
		}
	}
}
