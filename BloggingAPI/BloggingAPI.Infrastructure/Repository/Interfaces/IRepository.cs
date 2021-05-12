using System.Linq;
using System.Threading.Tasks;

namespace BloggingAPI.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Returns an IQueryable for TEntity
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Inserts provided TEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Updates provided TEntity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes provided TEntity
        /// </summary>
        /// <param name="entity"></param>
        Task<bool> DeleteAsync(TEntity entity);
    }
}
