using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AeWebApiDemo.Models;

namespace AeWebApiDemo.Query {
    public class CurrencyQuery : QueryBase, IQuery {
        public static string Currency = nameof(Currency);
        public CurrencyQuery(string option) : base(CurrencyQuery.Currency, hasOption: true) {
            base.Option = option;
        }

        async public Task<object[]> ExecuteAsync(TableauData data) {
            var currencyOption = base.Option;

            var client = HttpClientFactory.CreateClient("Currency Query Client");
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"http://api.exchangeratesapi.io/latest?access_key=[your access key]");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode) {
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
                var currencyJson = JsonDocument.Parse(responseString);
                var jsonRates = currencyJson.RootElement.GetProperty("rates");
                if (jsonRates.TryGetProperty(currencyOption, out var rateJson)) {
                    var rate = rateJson.GetDouble();
                    return await Task.FromResult(new object[1] { rate });
                } else {
                    throw new HttpRequestException("Currency Request Failed: "
                    + $"{currencyOption} not found.");
                }
            } else {
                throw new HttpRequestException("Currency Request Failed: "
                + $"StatusCode: {response.StatusCode}");
            }
        }
    }
}