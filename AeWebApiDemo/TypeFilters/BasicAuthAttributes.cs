using System;
using Microsoft.AspNetCore.Mvc;

namespace AeWebApiDemo.TypeFilters {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthAttribute : TypeFilterAttribute {
        public BasicAuthAttribute() : base(typeof(BasicAuthFilter)) { }
    }
}