using PostsApi.Application.Common.Interfaces;
using PostsApi.Infrastructure.Contexts;

namespace PostsApi.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{

    private readonly AppDbContext _dbContext;
    private Dictionary<Type, object> _repositories;
    
    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<Type, object>();
    }
    public int Save()
    {
        return _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IRepository<TEntity>)_repositories[typeof(TEntity)];
        }

        var repository = new Repository<TEntity>(_dbContext);
        _repositories.Add(typeof(TEntity), repository);
        return repository;
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }

}
