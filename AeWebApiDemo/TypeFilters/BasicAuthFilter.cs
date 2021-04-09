using System;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AeWebApiDemo.TypeFilters {
    public class BasicAuthFilter : IAuthorizationFilter {
        private const string Realm = "My Realm";

        public void OnAuthorization(AuthorizationFilterContext context) {
            try {
                string authorizationHeader = context.HttpContext.Request.Headers["Authorization"];
                if (authorizationHeader != null) {
                    var authHeaderValue = AuthenticationHeaderValue.Parse(authorizationHeader);
                    if (AuthenticationSchemes.Basic.ToString() == authHeaderValue.Scheme) {
                        var bytes = Convert.FromBase64String(authHeaderValue.Parameter ?? string.Empty);
                        var usernameAndPassword = Encoding.UTF8.GetString(bytes).Split(':', 2);
                        if (usernameAndPassword.Length == 2) {
                            var username = usernameAndPassword[0];
                            var password = usernameAndPassword[1];

                            if (IsAuthorized(context, username, password)) {
                                return;
                            }
                        }
                    }
                }

                ReturnUnauthorizedResult(context);
            } catch (FormatException) {
                ReturnUnauthorizedResult(context);
            }
        }

        public bool IsAuthorized(AuthorizationFilterContext context, string username, string password) {
            return username == "test" && password == "123";
        }

        private void ReturnUnauthorizedResult(AuthorizationFilterContext context) {
            // Return 401 and a basic authentication challenge
            context.HttpContext.Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{Realm}\"";
            context.Result = new UnauthorizedResult();
        }
    }
}