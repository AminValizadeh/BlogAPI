using Blog.DataLayer.Entities.Common;

namespace Blog.DataLayer.Repository
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetEntitiesQuery();

        Task<TEntity> GetEntityById(long entityId);

        Task AddEntity(TEntity entity);

        void UpdateEntity(TEntity entity);

        void RemoveEntity(TEntity entity);

        Task RemoveEntity(long entityId);

        Task SaveChanges();
    }
}