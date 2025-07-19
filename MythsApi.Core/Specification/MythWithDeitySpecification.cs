using MythsApi.Core.Entities; 

namespace MythsApi.Core.Specification
{
    public class MythWithDeitySpecification : BaseSpecification<Myth>
    {
        public MythWithDeitySpecification()
        {
            AddInclude(m => m.Deity);
        }

        public MythWithDeitySpecification(int id)
        {
            Criteria = m => m.Id == id;
            AddInclude(m => m.Deity);
        }
    }
}
