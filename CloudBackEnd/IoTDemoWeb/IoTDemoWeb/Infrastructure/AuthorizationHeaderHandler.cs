using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace IoTDemoWeb.Infrastructure
{
    public class AuthorizationHeaderHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Debug.WriteLine("Process request");
            // Call the inner handler.
            IEnumerable<string> apiKeyHeaderValues = null;
            if (request.Headers.TryGetValues("X-ApiKey", out apiKeyHeaderValues))
            {
                var apiKeyHeaderValue = apiKeyHeaderValues.First();

                // ... your authentication logic here ...
                string headerKey = ConfigurationManager.AppSettings["apiHeaderKey"].ToString();
                string clientName = ConfigurationManager.AppSettings["deviceName"].ToString();
                //var username = (apiKeyHeaderValue == "a98765432aWTFwtf89" ? "dorbelClient" : "OtherUser");
                var username = (apiKeyHeaderValue == headerKey ? clientName : "OtherUser");
                Debug.WriteLine(username);
                var usernameClaim = new Claim(ClaimTypes.Name, username);

                var identity = new ClaimsIdentity(new[] { usernameClaim }, "ApiKey");
                var principal = new ClaimsPrincipal(identity);

                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }
            }
            var response = await base.SendAsync(request, cancellationToken);
            Debug.WriteLine("Process response");
            return response;
        }
    }
}