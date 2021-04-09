using System;
using System.Net.Http;

namespace AeWebApiDemo.Query {
    public class QueryBase {
        private string name;
        public string Name => name;
        private bool hasOption;
        public bool HasOption => hasOption;
        private Type[] types;
        public Type[] Types => types;
        public string Option { get; set; }
        public IHttpClientFactory HttpClientFactory { get; set; }

        public QueryBase(string name, bool hasOption, params Type[] types) {
            this.name = name;
            this.hasOption = hasOption;
            this.types = types;
        }
    }
}