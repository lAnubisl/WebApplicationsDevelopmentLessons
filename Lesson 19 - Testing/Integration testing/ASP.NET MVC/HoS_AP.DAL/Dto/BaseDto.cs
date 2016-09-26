using System;

namespace HoS_AP.DAL.Dto
{
    [Serializable]
    public abstract class BaseDto
    {
        public Guid Id { get; set; }
    }
}