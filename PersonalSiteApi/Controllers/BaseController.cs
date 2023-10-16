using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PersonalSiteApi.EntityFramework;

namespace PersonalSiteApi.Controllers
{
    public class BaseController : Controller
    {
        public readonly IConfiguration _config;
        public readonly PersonalSiteContext _context;
        public string _language = "";
        public BaseController(IConfiguration config, PersonalSiteContext context)
        {
            _config = config;
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (Request.Headers.ContainsKey("preferred-language"))
            {
                _language = Request.Headers.First(x => x.Key.ToLower() == "preferred-language").Value.ToString();
            }
        }
    }
}
