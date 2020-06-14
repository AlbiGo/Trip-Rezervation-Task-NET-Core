using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TourReservationAPI.Interface
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> SaveAsync(T entity);
    
    
    }
}
