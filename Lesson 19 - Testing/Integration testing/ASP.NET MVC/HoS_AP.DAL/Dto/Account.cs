using System;

namespace HoS_AP.DAL.Dto
{
    [Serializable]
    public class Account : BaseDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}