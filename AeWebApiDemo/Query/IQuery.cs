using System;
using System.Net.Http;
using System.Threading.Tasks;
using AeWebApiDemo.Models;

namespace AeWebApiDemo.Query {
    public interface IQuery {
        //TODO: use generic return type T for Execute method
        Task<object[]> ExecuteAsync(TableauData data);
        bool HasOption { get; }
        Type[] Types { get; }
        IHttpClientFactory HttpClientFactory { get; set; }
    }
}