using System.Collections.Generic;
using System.Linq;
using DeleporterCore.Client;
using HoS_AP.DAL.Dto;
using HoS_AP.DAL.Repository;
using Moq;

namespace HoS_AP.Web.Tests
{
    public class DeleporterMockStateAdapter : IStateAdapter
    {
        public void InitAccounts(List<Account> accounts)
        {
            Deleporter.Run(() =>
            {
                var repository = new Mock<IGenericRepository<Account>>();
                repository.Setup(x => x.Entities).Returns(accounts.AsQueryable());
                Global.Container.Mock<IGenericRepository<Account>>(repository.Object);
            });
        }

        public void InitCharacters(List<Character> characters)
        {
            Deleporter.Run(() =>
            {
                var repository = new Mock<IGenericRepository<Character>>();
                repository.Setup(x => x.Entities).Returns(characters.AsQueryable());
                Global.Container.Mock<IGenericRepository<Character>>(repository.Object);
            });
        }
    }
}