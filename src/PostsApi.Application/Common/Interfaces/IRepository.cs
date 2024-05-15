using System.Linq.Expressions;

namespace PostsApi.Application.Common.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    IQueryable<T>  GetAllAsQueryAble();
    Task Add(T entity);
    void Delete(T entity);
    void Update(T entity);
    IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression);

}

