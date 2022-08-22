using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace WebAPI.Infrastructure
{
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    //[Auth]
    public class ApiController : Controller
    {
        protected int UserId;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            UserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value ?? "0");
            base.OnActionExecuting(context);
        }
    }
}
