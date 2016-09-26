using System;
using System.Collections.Generic;
using System.Linq;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.DAL.Dto;
using HoS_AP.DAL.Repository;
using log4net;

namespace HoS_AP.DAL.Dao
{
    internal class CharacterDao : ICharacterDao
    {
        private static readonly ILog Logger = LogManager.GetLogger("CharacterDao");

        private readonly IGenericRepository<Character> repository;

        public CharacterDao(IGenericRepository<Character> repository)
        {
            Logger.InfoFormat("Creating new instance of CharacterDao with repository of type {0}", repository.GetType());
            this.repository = repository;
        }

        ICollection<Character> ICharacterDao.Load()
        {
            return repository.Entities.ToList();
        }

        Character ICharacterDao.Load(string name)
        {
            return repository.Entities.FirstOrDefault(x => x.Name == name);
        }

        void ICharacterDao.Save(Character character)
        {
            repository.Save(character);
        }

        Character ICharacterDao.Load(Guid Id)
        {
            return repository.Entities.FirstOrDefault(x => x.Id == Id);
        }
    }
}