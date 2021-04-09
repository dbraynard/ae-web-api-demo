using System;
using System.Threading.Tasks;
using AeWebApiDemo.Models;

namespace AeWebApiDemo.Query {
    public class TrendingQuery : QueryBase, IQuery {
        public TrendingQuery() : base("Trending", hasOption: false, typeof(string)) { }

        async public Task<object[]> ExecuteAsync(TableauData data) {
            //trending search term
            var arg1Name = TableauKeywords.Arg(0);
            if (!data.ContainsKey(arg1Name)) {
                throw new MissingFieldException("Trending Query cannot run. Search term field is missing.");
            }

            //arg data
            var searchTerms = data[arg1Name].StringData;
            var results = new object[searchTerms.Length];

            var r = new Random();
            for (var i = 0; i < results.Length; i++) {
                //simulate changing exposure per hour.
                var hashOfNames = searchTerms[i].GetHashCode();
                var modOfHash = hashOfNames % 50;
                var randomNumber = r.Next(1, 100000);
                var shippingRate = 10 + modOfHash + randomNumber;
                results[i] = shippingRate;
            }

            return await Task.FromResult(results);
        }
    }
}