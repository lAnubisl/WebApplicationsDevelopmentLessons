using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HoS_AP.DAL.Dto;
using Newtonsoft.Json;

namespace HoS_AP.DAL.Dao
{
    internal class FileSystemRepository
    {
        private readonly string basePath;
        private const string defaultAccount = "[{'UserName':'Megan', 'Password':'1000:KmPsJ6b8qrf5d0flq2JZ7pZfXFiIZWfK:VeuXPEkDBL5B3rCCfE7OPVLumsX0NAJT'}]";
        private List<Account> accounts;
        private List<Character> characters;
        private const string AccountsFileName = "Accounts.json";
        private const string CharactersFileName = "Characters.json";

        public FileSystemRepository()
        {
            var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            basePath = Path.GetDirectoryName(path);
        }

        protected IQueryable<Account> Accounts
        {
            get
            {
                if (accounts == null)
                {
                    accounts = Load<List<Account>>(AccountsFileName, defaultAccount);
                }

                return accounts.AsQueryable();
            }
        }

        protected IQueryable<Character> Characters
        {
            get
            {
                if (characters == null)
                {
                    characters = Load<List<Character>>(CharactersFileName, string.Empty);
                }

                return characters.AsQueryable();
            }
        }

        protected void Save(Character character)
        {
            var characterToRemove = characters.FirstOrDefault(x => x.Id == character.Id);
            if (characterToRemove != null)
            {
                characters.Remove(characterToRemove);
            }

            characters.Add(character);
            PersistCharacters();
        }

        private void PersistCharacters()
        {
            File.WriteAllText(Path.Combine(basePath, CharactersFileName), JsonConvert.SerializeObject(characters));
        }

        private T Load<T>(string fileName, string defaultFileContent) where T : new()
        {
            var path = Path.Combine(basePath, fileName);
            if (!File.Exists(path))
            {
                File.WriteAllText(path, defaultFileContent);
            }

            var result = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            if (result == null) result = new T();
            return result;
        }
    }
}