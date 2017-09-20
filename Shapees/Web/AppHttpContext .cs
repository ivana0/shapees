using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Threading;


//Tutorial for more info: https://stackoverflow.com/questions/37785767/how-to-access-the-session-in-asp-net-core-via-static-variable
namespace Shapees.Web
{
    public static class AppHttpContext
    {
        static IServiceProvider services = null;

        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                services = value;
            }
        }

        /// <summary>
        /// Provides static access to the current HttpContext
        /// </summary>
        public static HttpContext Current
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }
    }
}
