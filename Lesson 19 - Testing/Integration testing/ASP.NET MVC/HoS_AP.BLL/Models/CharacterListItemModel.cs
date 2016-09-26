using System;
using HoS_AP.DAL.Dto;
using HoS_AP.Misc;

namespace HoS_AP.BLL.Models
{
    public class CharacterListItemModel
    {
        private readonly string name;
        private readonly decimal price;
        private readonly DateTime created;
        private readonly CharacterTypes type;
        private readonly bool active, deleted;

        internal CharacterListItemModel(Character character)
        {
            name = character.Name;
            price = character.Price;
            created = character.Created;
            type = character.Type;
            active = character.Active;
            deleted = character.Deleted;
        }

        public string Name
        {
            get { return name; }
        }

        public decimal Price
        {
            get { return price; }
        }

        public DateTime Created
        {
            get { return created; }
        }

        public CharacterTypes Type
        {
            get { return type; }
        }

        public bool Deleted
        {
            get { return deleted; }
        }

        public bool Active
        {
            get { return active; }
        }
    }
}