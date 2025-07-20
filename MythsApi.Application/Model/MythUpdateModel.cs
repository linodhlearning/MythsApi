using System.ComponentModel.DataAnnotations;

namespace MythsApi.Application.Model
{
    public class MythUpdateModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Story { get; set; } = string.Empty;

        public string OriginPeriod { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;

        [Required]
        public int DeityId { get; set; }
    }
}
