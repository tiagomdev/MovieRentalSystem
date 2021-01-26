using Microsoft.EntityFrameworkCore;
using MovieRentalSystem.Core.Entities;
using MovieRentalSystem.Core.Interfaces.Repositories;
using MovieRentalSystem.Infra.Data.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRentalSystem.Infra.Data.Repositories
{
    public class RepositoryBase : IRepository
    {
        protected readonly MovieRentalSystemContext context;

        public RepositoryBase(MovieRentalSystemContext context)
        {
            this.context = context;
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return context.Set<TEntity>().AsQueryable();
        }

        public async Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EntityBase
        {
            await context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EntityBase
        {
            context.Entry(entity).State = EntityState.Modified;
            context.Update(entity);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : EntityBase
        {
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
