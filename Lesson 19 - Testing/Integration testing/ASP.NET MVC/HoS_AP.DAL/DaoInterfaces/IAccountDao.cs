using HoS_AP.DAL.Dto;

namespace HoS_AP.DAL.DaoInterfaces
{
    public interface IAccountDao
    {
        Account Load(string userName);
    }
}