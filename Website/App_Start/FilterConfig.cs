using System.Web.Mvc;
using Website.Localization;

namespace Website
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LanguageFilterAttribute());
        }
    }
}