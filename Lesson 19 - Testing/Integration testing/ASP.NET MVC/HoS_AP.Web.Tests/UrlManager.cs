using System.Collections.Generic;

namespace HoS_AP.Web.Tests
{
    internal static class UrlManager
    {
        private static readonly IDictionary<string, string> knownPages = new Dictionary<string, string>
        {
            {"Login", GetAbsoluteUrl("/")},
            {"Listing", GetAbsoluteUrl("/characters")},
            {"Add", GetAbsoluteUrl("/characters/add")}
        };

        internal static string GetPage(string pageName)
        {
            if (pageName.StartsWith("Edit "))
            {
                var entity = pageName.Replace("Edit ", string.Empty);
                return GetAbsoluteUrl(string.Format("/characters/{0}/edit", entity));
            }

            return knownPages[pageName];
        }

        private static string GetAbsoluteUrl(string relativeUrl)
        {
            if (!relativeUrl.StartsWith("/"))
            {
                relativeUrl = "/" + relativeUrl;
            }
            return string.Format("http://localhost:{0}{1}", Constants.IisPort, relativeUrl);
        }
    }
}