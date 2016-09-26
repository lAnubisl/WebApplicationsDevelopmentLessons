using System;
using System.Collections.Generic;
using HoS_AP.DAL.Dto;

namespace HoS_AP.DAL.DaoInterfaces
{
    public interface ICharacterDao
    {
        ICollection<Character> Load();

        Character Load(string name);

        void Save(Character character);

        Character Load(Guid Id);
    }
}