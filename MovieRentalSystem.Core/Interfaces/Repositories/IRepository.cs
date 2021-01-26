using MovieRentalSystem.Core.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRentalSystem.Core.Interfaces.Repositories
{
    public interface IRepository
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;

        Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EntityBase;

        Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EntityBase;

        Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EntityBase;
    }
}
