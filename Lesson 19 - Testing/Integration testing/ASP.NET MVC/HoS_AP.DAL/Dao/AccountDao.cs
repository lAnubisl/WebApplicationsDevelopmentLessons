using System.Linq;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.DAL.Dto;
using HoS_AP.DAL.Repository;

namespace HoS_AP.DAL.Dao
{
    internal class AccountDao : IAccountDao
    {
        private readonly IGenericRepository<Account> repository;

        public AccountDao(IGenericRepository<Account> repository)
        {
            this.repository = repository;
        }

        Account IAccountDao.Load(string userName)
        {
            return repository.Entities.FirstOrDefault(x => x.UserName == userName);
        }
    }
}