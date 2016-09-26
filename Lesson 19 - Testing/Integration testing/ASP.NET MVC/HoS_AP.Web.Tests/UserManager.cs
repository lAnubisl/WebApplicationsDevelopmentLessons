using System.Collections.Generic;

namespace HoS_AP.Web.Tests
{
    internal static class UserManager
    {
        private static readonly IDictionary<string, string> knownUsers = new Dictionary<string, string>
        {
            {"Megan", "123456"}
        };

        internal static string GetPassword(string username)
        {
            return knownUsers[username];
        }
    }
}