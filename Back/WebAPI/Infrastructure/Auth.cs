using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AuthAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            foreach (var filterDescriptors in context.ActionDescriptor.FilterDescriptors)
            {
                if (filterDescriptors.Filter.GetType() == typeof(AllowAnonymousFilter))
                {
                    return;
                }
            }

            var user = context.HttpContext.User;

            var userId = int.Parse(user.Claims.FirstOrDefault(x => x.Type == "userId")?.Value ?? "0");
            var anonymousCode = user.Claims.FirstOrDefault(x => x.Type == "anonymousCode")?.Value;
            if (anonymousCode == "0")
            {
                anonymousCode = null;
            }
            if (!user.Identity.IsAuthenticated || (userId == 0 && string.IsNullOrEmpty(anonymousCode)))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

        }
    }
}
