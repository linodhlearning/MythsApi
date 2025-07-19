using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MythsApi.Application.Interfaces;
using MythsApi.Application.Model;
using MythsApi.Core.Entities;
using MythsApi.Infrastructure.Data;

namespace MythsApi.Infrastructure.Services
{
    public class MythService : IMythService
    {
        private readonly MythsDbContext _context;
        private readonly IMapper _mapper;

        public MythService(MythsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MythModel>> GetAllAsync()
        {
            var myths = await _context.Myths.Include(m => m.Deity).ToListAsync();
            return _mapper.Map<IEnumerable<MythModel>>(myths);
        }

        public async Task<MythModel?> GetByIdAsync(int id)
        {
            var myth = await _context.Myths.FindAsync(id);
            return myth == null ? null : _mapper.Map<MythModel>(myth);
        }

        public async Task<MythModel> CreateAsync(MythModel dto)
        {
            var myth = _mapper.Map<Myth>(dto);
            _context.Myths.Add(myth);
            await _context.SaveChangesAsync();
            return _mapper.Map<MythModel>(myth);
        }

        public async Task UpdateAsync(int id, MythModel dto)
        {
            var myth = await _context.Myths.FindAsync(id);
            if (myth != null)
            {
                _mapper.Map(dto, myth);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var myth = await _context.Myths.FindAsync(id);
            if (myth != null)
            {
                _context.Myths.Remove(myth);
                await _context.SaveChangesAsync();
            }
        }
    }
}
