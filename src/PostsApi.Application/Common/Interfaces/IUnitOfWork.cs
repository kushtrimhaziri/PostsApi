namespace PostsApi.Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    int Save();
}
