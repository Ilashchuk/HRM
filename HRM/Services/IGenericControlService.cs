namespace HRM.Services
{
    public interface IGenericControlService<T>
    {
        public Task<List<T>> GetListAsync();
        public Task<T?> GetByIdAsync(int? id);
        public Task AddAsync(T t);
        public Task DeleteAsync(T t);
        public Task UpdateAsync(T t);
        public bool NotEmpty();
        public bool Exists(int id);
    }
}
