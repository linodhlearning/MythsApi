using MythsApi.Application.Interfaces;
using MythsApi.Core.Entities;
using MythsApi.Infrastructure.Data; 

namespace MythsApi.Infrastructure.Repositories
{
    public class MythRepository : GenericRepository<Myth>, IMythRepository
    {
        public MythRepository(MythsDbContext context) : base(context) { }
    }
}
