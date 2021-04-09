using System;

namespace AeWebApiDemo.Query {
    public static class QueryFactory {
        public static IQuery GetQuery(string script) {
            var (name, option) = GetQueryNameAndOption(script);
            return name switch
            {
                "Currency" => new CurrencyQuery(option),
                "Shipping" => new ShippingRateQuery(),
                "Trending" => new TrendingQuery(),
                _ => throw new NotImplementedException()
            };
        }

        private static (string name, string option) GetQueryNameAndOption(string script) {
            var parts = script.Split(':');
            return (parts[0], parts.Length > 1 ? parts[1] : null);
        }
    }
}