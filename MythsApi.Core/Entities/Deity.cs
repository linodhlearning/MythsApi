using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MythsApi.Core.Entities
{
    public class Deity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PantheonId { get; set; }
        public Pantheon Pantheon { get; set; } = null!;
        public ICollection<Myth> Myths { get; set; } = new List<Myth>();
    }
}
