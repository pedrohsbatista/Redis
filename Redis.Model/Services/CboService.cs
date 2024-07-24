using Redis.Model.Entities;
using Redis.Model.Interfaces;

namespace Redis.Model.Services
{
    public class CboService
    {
        private readonly IRepository<Cbo> _cboRepository;

        public CboService(IRepository<Cbo> cboRepository)
        {
            _cboRepository = cboRepository;                
        }

        public async Task<List<Cbo>> GetAll()
        {
            return await _cboRepository.GetAll();
        }

        public async Task<Cbo> GetById(Guid id)
        {
            return await _cboRepository.GetById(id);
        }
    }
}
