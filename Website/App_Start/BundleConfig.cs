using System.Web.Optimization;

namespace Website
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/global").Include(
                "~/content/normalize.css",
                "~/content/foundation.css",
                "~/content/icomoon.css",
                "~/content/toggle-switch.css",
                "~/content/toggle-switch-states.css",
                "~/content/toggle-switch-foundry.css",
                "~/content/application.css"
            ));

            bundles.Add(new ScriptBundle("~/js/global").Include(
                "~/scripts/picturefill.js"
            ));

            bundles.Add(new ScriptBundle("~/js/lte-ie7").Include(
                "~/scripts/lte-ie7.js"
            ));

            bundles.Add(new ScriptBundle("~/js/app").Include(
                "~/scripts/jquery-2.0.0.js",
                "~/scripts/jquery.validate.js",
                "~/scripts/jquery.validate.unobtrusive.js"
            ));
        }
    }
}