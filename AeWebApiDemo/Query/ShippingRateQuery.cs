using System;
using System.Threading.Tasks;
using AeWebApiDemo.Models;

namespace AeWebApiDemo.Query {
    public class ShippingRateQuery : QueryBase, IQuery {
        public ShippingRateQuery() : base("ShippingRate", hasOption: false,
                typeof(string), typeof(string)) { }

        async public Task<object[]> ExecuteAsync(TableauData data) {
            //country and region code
            var arg1Name = TableauKeywords.Arg(0);
            var arg2Name = TableauKeywords.Arg(1);
            if (!data.ContainsKey(arg1Name) || !data.ContainsKey(arg2Name)) {
                throw new MissingFieldException("Shipping Rate Query cannot run. "
                + "Some fields are missing.");
            }

            //arg data
            var countryCodes = data[arg1Name].StringData;
            var postalCodes = data[arg2Name].StringData;
            var results = new object[countryCodes.Length];

            var r = new Random();

            for (var i = 0; i < results.Length; i++) {
                //simulate changing shipping rates.
                var hashOfNames = (countryCodes[i], postalCodes[i]).GetHashCode();
                var modOfHash = hashOfNames % 50;
                var randomNumber = r.Next(1, 100) / 10d;
                var shippingRate = 100 + modOfHash + randomNumber;
                results[i] = shippingRate;
            }

            return await Task.FromResult(results);
        }
    }
}