using System;
using HoS_AP.BLL.Models;
using HoS_AP.DAL.Dto;

namespace HoS_AP.BLL.Mappers
{
    internal static class CharacterMapper
    {
        internal static void MapTo(this CharacterEditModel model, ref Character entity)
        {
            if (model.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
                entity.Created = DateTime.Now;
            }

            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.Type = model.Type;
            entity.Active = model.Active;
        }
    }
}