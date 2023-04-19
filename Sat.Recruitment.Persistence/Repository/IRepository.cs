using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Persistence.Repository
{
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// It retrieves a collection of type TEntity
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// It retrieves a entity of type TEntity
        /// </summary>
        /// <param name="id">Record Id.</param>
        /// <returns>Task<TEntity></returns>
        Task<TEntity> Get(int id);

        /// <summary>
        /// It creates a new record of type TEntity
        /// </summary>
        /// <param name="entity">Generic TEntity</param>
        /// <returns>Task</returns>
        Task Add(TEntity entity);

        /// <summary>
        /// It updates a record of type TEntity
        /// </summary>
        /// <param name="entity">Generic TEntity</param>
        /// <returns>Task</returns>
        void Update(TEntity entity);

        /// <summary>
        /// It delete a record of type TEntity
        /// </summary>
        /// <param name="id">Record Id.</param>
        /// <returns>Task</returns>
        Task Delete(int id);

        /// <summary>
        /// Save changes in the database.
        /// </summary>
        /// <returns>Task</returns>
        Task Save();
    }
}
