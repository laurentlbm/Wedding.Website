using System.Threading;

namespace Website.Localization
{
    public static class CultureHelper
    {
        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture.Name;
        }

        public static string GetCurrentNeutralCulture()
        {
            return GetNeutralCulture(Thread.CurrentThread.CurrentCulture.Name);
        }

        public static string GetNeutralCulture(string name)
        {
            if (name.Length < 2)
                return name;

            return name.Substring(0, 2); // Read first two chars only. E.g. "en", "es"
        }

        public static void SetCurrentCulture(string cultureName)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }
    }
}