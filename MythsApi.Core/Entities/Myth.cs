namespace MythsApi.Core.Entities
{
    public class Myth
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Story { get; set; } = string.Empty;
        public string OriginPeriod { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int DeityId { get; set; }
        public Deity Deity { get; set; } = null!;
    }
}
