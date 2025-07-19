namespace MythsApi.Core.Entities
{ 
    public class Pantheon
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Deity> Deities { get; set; } = new List<Deity>();
    } 
}
