﻿namespace MythsApi.Application.Model
{

    public class MythModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Story { get; set; } = string.Empty;
        public string OriginPeriod { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public int DeityId { get; set; }
        public string? DeityName { get; set; }
    } 
}
