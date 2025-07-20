using MythsApi.Application.Model;
namespace MythsApi.Application.Interfaces
{
    public interface IMythService
    {
        Task<IEnumerable<MythModel>> GetAllAsync();
        Task<MythModel?> GetByIdAsync(int id);
        Task<MythModel> CreateAsync(MythCreateModel model);
        Task UpdateAsync(int id, MythUpdateModel model);
        Task DeleteAsync(int id);
    }
}
