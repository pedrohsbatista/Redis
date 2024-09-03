namespace Redis.Model.Interfaces
{
    public interface ICache<T>
    {
        public Task SetSingle(string key, T entity, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null);

        public Task<T> GetSingle(string key);

        public Task SetAll(string key, List<T> entities, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null);

        public Task<List<T>> GetAll(string key);
    }
}
