using AutoMapper;
using MythsApi.Application.Interfaces;
using MythsApi.Application.Model;
using MythsApi.Core.Entities;
using MythsApi.Core.Specification;
namespace MythsApi.Infrastructure.Services
{
    public class MythService : IMythService
    {
        private readonly IMythRepository _repository;
        private readonly IMapper _mapper;

        public MythService(IMythRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MythModel>> GetAllAsync()
        {
            var myths = await _repository.ListAsync(new MythWithDeitySpecification());
            return _mapper.Map<IEnumerable<MythModel>>(myths);
        }

        public async Task<MythModel?> GetByIdAsync(int id)
        {
            var myth = await _repository.GetAsync(new MythWithDeitySpecification(id));
            return myth == null ? null : _mapper.Map<MythModel>(myth);
        }

        public async Task<MythModel> CreateAsync(MythModel dto)
        {
            var myth = _mapper.Map<Myth>(dto);
            await _repository.AddAsync(myth);
            return _mapper.Map<MythModel>(myth);
        }

        public async Task UpdateAsync(int id, MythModel dto)
        {
            var existing = await _repository.GetAsync(new MythWithDeitySpecification(id));
            if (existing != null)
            {
                _mapper.Map(dto, existing);
                await _repository.UpdateAsync(existing);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetAsync(new MythWithDeitySpecification(id));
            if (existing != null)
                await _repository.DeleteAsync(existing);
        }
    }
}
