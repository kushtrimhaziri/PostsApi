using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PostsApi.Application.Common.Interfaces;
using PostsApi.Infrastructure.Contexts;

namespace PostsApi.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext context)
    {
        _dbContext = context;
    }

    public async Task<T> GetById(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public IQueryable<T> GetAllAsQueryAble()
    {
        return _dbContext.Set<T>().AsQueryable();
    }

    public async Task Add(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }
    
    public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
    {
        return _dbContext.Set<T>().Where(expression).AsQueryable();
    }
}
