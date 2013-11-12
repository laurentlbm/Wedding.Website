using System;
using System.Threading;
using System.Web.Mvc;

namespace Website.Localization
{
    public class LanguageFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            string cultureName = "en";
            if (request.Url.Host.EndsWith("sheetaletlaurent.com", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(request.QueryString.Get("lang"), "fr", StringComparison.OrdinalIgnoreCase))
            {
                cultureName = "fr";
            }

            CultureHelper.SetCurrentCulture(cultureName);

            base.OnActionExecuting(filterContext);
        }
    }
}