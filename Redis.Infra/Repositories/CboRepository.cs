using Redis.Model.Interfaces;

namespace Redis.Infra.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        public async Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
