using System.Linq.Expressions;
using Habit.Domain.Entities.Abstraction;

namespace Habit.Application.Repositories;

/// <summary>
/// Интерфейс репозитория для доступа к данным.
/// </summary>
/// <typeparam name="T">Тип сущности.</typeparam>
public interface IRepository<T> where T : EntityBase
{
    /// <summary>
    /// Получает сущность по идентификатору асинхронно.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <returns>Сущность.</returns>
    IQueryable<T> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Получает список всех сущностей асинхронно.
    /// </summary>
    /// <returns>Список сущностей.</returns>
    IQueryable<T> GetListAsync();
    
    /// <summary>
    /// Получает список сущностей, удовлетворяющих предикату асинхронно.
    /// </summary>
    /// <param name="predicate">Предикат для фильтрации.</param>
    /// <returns>Список сущностей.</returns>
    IQueryable<T> GetListAsync(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Проверяет, существует ли сущность, удовлетворяющая предикату, асинхронно.
    /// </summary>
    /// <param name="predicate">Предикат для фильтрации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если сущность существует, в противном случае - false.</returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Добавляет сущность асинхронно.
    /// </summary>
    /// <param name="entity">Добавляемая сущность.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор добавленной сущности.</returns>
    Task<Guid> AddAsync(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Добавляет коллекцию сущностей асинхронно.
    /// </summary>
    /// <param name="entities">Коллекция добавляемых сущностей.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Обновляет сущность асинхронно.
    /// </summary>
    /// <param name="entity">Обновляемая сущность.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Обновляет коллекцию сущностей асинхронно.
    /// </summary>
    /// <param name="entities">Коллекция обновляемых сущностей.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}