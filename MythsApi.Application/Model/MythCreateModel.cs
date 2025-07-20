using System.ComponentModel.DataAnnotations;
namespace MythsApi.Application.Model
{
    public class MythCreateModel
    {
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
