namespace BookShop.Services.Abstractions;

public interface IEntityService<T>
{
    Task AddAsync(T entity);
    Task RemoveByIdAsync(long entityId);
    Task RemoveAsync(T entity);
    Task ModifyAsync(T entity);
    Task<T> GetByIdAsync(long entityId);
    Task<List<T>> GetAllAsync();
    Task ClearAsync();
}
