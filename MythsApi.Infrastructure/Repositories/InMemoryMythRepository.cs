using MythsApi.Application.Interfaces; 
using MythsApi.Core.Entities;
using MythsApi.Infrastructure.Data;
namespace MythsApi.Infrastructure.Repositories
{
    public class InMemoryMythRepository : GenericRepository<Myth>, IMythRepository
    {
        public InMemoryMythRepository(MythsDbContext context) : base(context) { }
    }
}
