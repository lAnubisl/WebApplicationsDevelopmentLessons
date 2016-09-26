using System.Collections.Generic;
using HoS_AP.DAL.Dto;

namespace HoS_AP.Web.Tests
{
    internal interface IStateAdapter
    {
        void InitAccounts(List<Account> accounts);
        void InitCharacters(List<Character> characters);
    }
}