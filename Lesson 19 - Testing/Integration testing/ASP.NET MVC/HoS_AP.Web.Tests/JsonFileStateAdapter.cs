using System.Collections.Generic;
using System.IO;
using HoS_AP.DAL.Dto;
using Newtonsoft.Json;

namespace HoS_AP.Web.Tests
{
    public class JsonFileStateAdapter : IStateAdapter
    {
        public void InitAccounts(List<Account> accounts)
        {
            var path = TestRunConfiguration.GetApplicationPath();
            path = Path.Combine(path, "bin") + "/Accounts.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(accounts));
        }

        public void InitCharacters(List<Character> characters)
        {
            var path = TestRunConfiguration.GetApplicationPath();
            path = Path.Combine(path, "bin") + "/Characters.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(characters));
        }
    }
}