using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloggingAPI.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly BloggingDbContext DbContext;
        public Repository(BloggingDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return DbContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception($"Couldn't retrieve entities: {ex.InnerException.Message}");

                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(InsertAsync)} entity must not be null");

            try
            {
                await DbContext.AddAsync(entity).ConfigureAwait(false);
                var itemsAdded = await DbContext.SaveChangesAsync().ConfigureAwait(false);

                if (itemsAdded > 0)
                    return entity;

                return null;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception($"{nameof(entity)} could not be saved: {ex.InnerException.Message}");

                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");

            try
            {
                DbContext.Update(entity);
                var updatedItems = await DbContext.SaveChangesAsync().ConfigureAwait(false);

                if (updatedItems > 0)
                    return entity;

                return null;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception($"{nameof(entity)} could not be updated: {ex.InnerException.Message}");

                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException($"{nameof(DeleteAsync)} entity must not be null");

            try
            {
                DbContext.Remove(entity);
                var itemsRemoved =  await DbContext.SaveChangesAsync().ConfigureAwait(false);

                return itemsRemoved > 0;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    throw new Exception($"{nameof(entity)} could not be deleted: {ex.InnerException.Message}");

                throw new Exception($"{nameof(entity)} could not be deleted: {ex.Message}");
            }
        }
    }
}
