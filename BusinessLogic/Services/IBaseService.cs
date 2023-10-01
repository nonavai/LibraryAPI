namespace BusinessLogic.Services;

public interface IBaseService<T>
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T model);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}