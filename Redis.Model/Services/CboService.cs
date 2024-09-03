using Redis.Model.Entities;
using Redis.Model.Interfaces;

namespace Redis.Model.Services
{
    public class CboService
    {
        private readonly IRepository<Cbo> _cboRepository;
        private readonly ICache<Cbo> _cache;

        public CboService(IRepository<Cbo> cboRepository, ICache<Cbo> cache)
        {
            _cboRepository = cboRepository;
            _cache = cache;
        }

        public async Task<List<Cbo>> GetAll()
        {
            var cbos = await _cache.GetAll(nameof(Cbo));

            if (cbos.Any())
                return cbos;

            cbos = await _cboRepository.GetAll();

            if (!cbos.Any())
                return new List<Cbo>();

            await _cache.SetAll(nameof(Cbo), cbos, TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(30));

            return cbos;
        }

        public async Task<Cbo> GetById(Guid id)
        {
            var cbo = await _cache.GetSingle(id.ToString());

            if (cbo != null)
                return cbo;

            cbo = await _cboRepository.GetById(id);

            if (cbo == null)
                return null;

            await _cache.SetSingle(id.ToString(), cbo, TimeSpan.FromSeconds(60), TimeSpan.FromSeconds(30));

            return cbo;
        }

        public async Task Insert(Cbo cbo)
        {
            await _cboRepository.Insert(cbo);
        }
    }
}
