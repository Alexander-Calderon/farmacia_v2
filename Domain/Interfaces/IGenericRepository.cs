using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Interfaces;

    //se comenta para poder permitir entidades sin que hereden de baseentity 
    // como las que usan pk compuestas.
    public interface IGenericRepository<T> //where T :BaseEntity 
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
    }
