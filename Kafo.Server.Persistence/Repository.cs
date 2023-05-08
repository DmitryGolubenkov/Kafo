using System.Collections;
using System.Linq.Expressions;
using Kafo.Application.Interfaces;
using Kafo.Domain;

namespace Kafo.Server.Persistence;
/// <summary>
/// Implementation of repository pattern using Entity Framework Core.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public class Repository<T> : IRepository<T> where T : EntityBase
{
    private readonly AppDbContext _dbContext;
    private readonly IQueryable<T> _entities;

    public Type ElementType => _entities.ElementType;

    public Expression Expression => _entities.Expression;

    public IQueryProvider Provider => _entities.Provider;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _entities = _dbContext.Set<T>();
    }

    /// <summary>
    /// Adds entity of type <typeparamref name="T"/> to database without saving.
    /// </summary>
    public void Add(T entity)
    {
        _dbContext.Set<T>().Add(entity);
    }

    /// <summary>
    /// Removes entity of type <typeparamref name="T"/> from database without saving.
    /// </summary>
    public void Remove(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    /// <summary>
    /// Returns all entities of type <typeparamref name="T"/> stored in database.
    /// </summary>
    /// <returns>IEnumerable that contains all entities in database.</returns>
    public IEnumerable<T> GetAll()
    {
        return _entities.AsEnumerable();
    }

    /// <summary>
    /// Returns stored entity of type <typeparamref name="T"/> with Id equal to <paramref name="id"/>.
    /// </summary>
    /// <param name="id">Id of the entity</param>
    /// <returns>Entity of type <typeparamref name="T"/> with Id equal to <paramref name="id"/>, or null if no entity was found</returns>
    public T? GetById(Guid id)
    {
        return _entities.Where(x => x.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Updates entity of type <typeparamref name="T"/> in database without saving.
    /// </summary>
    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    /// <summary>
    /// Saves changes in repository of type <typeparamref name="T"/>.
    /// </summary>
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _entities.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return GetAsyncEnumerator(cancellationToken);
    }
}