namespace Redis.Model.Interfaces
{
    public interface IRepository<T>
    {
        public Task<List<T>> GetAll();

        public Task<T> GetById(Guid id);

        public Task Insert(T entity);
    }
}
