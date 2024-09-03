using Microsoft.EntityFrameworkCore;
using Redis.Model.Interfaces;

namespace Redis.Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {            
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task Insert(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
