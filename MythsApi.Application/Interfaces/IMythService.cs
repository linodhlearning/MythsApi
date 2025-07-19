using MythsApi.Application.Model;
namespace MythsApi.Application.Interfaces
{
    public interface IMythService
    {
        Task<IEnumerable<MythModel>> GetAllAsync();
        Task<MythModel?> GetByIdAsync(int id);
        Task<MythModel> CreateAsync(MythModel myth);
        Task UpdateAsync(int id, MythModel myth);
        Task DeleteAsync(int id);
    }
}
