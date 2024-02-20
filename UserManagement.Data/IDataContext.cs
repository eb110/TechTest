using System.Linq;

namespace UserManagement.Data;

public interface IDataContext
{
    /// <summary>
    /// Get a list of items
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;

    /// <summary>
    /// Create a new item
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    void Create<TEntity>(TEntity entity) where TEntity : class;

    /// <summary>
    /// Update an existing item matching the ID
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    void Update<TEntity>(TEntity entity) where TEntity : class;

    void Delete<TEntity>(TEntity entity) where TEntity : class;
    /// <summary>
    /// Get an item by its id
    /// </summary>
    /// <typeparam Id="Long"></typeparam>
    /// <param name="Id"></param>
    /// <returns></returns>
    TEntity? GetById<TEntity>(long Id) where TEntity : class;
}
