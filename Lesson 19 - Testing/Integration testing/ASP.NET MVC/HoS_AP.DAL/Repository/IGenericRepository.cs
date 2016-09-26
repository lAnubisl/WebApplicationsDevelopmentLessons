using System.Linq;
using HoS_AP.DAL.Dto;

namespace HoS_AP.DAL.Repository
{
    public interface IGenericRepository<T> where T : BaseDto
    {
        IQueryable<T> Entities { get; }

        void Save(T entity);
    }
}