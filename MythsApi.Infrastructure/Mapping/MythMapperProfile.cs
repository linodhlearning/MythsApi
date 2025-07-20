using AutoMapper;
using MythsApi.Application.Model;
using MythsApi.Core.Entities;

namespace MythsApi.Infrastructure.Mapping
{
    public class MythMapperProfile : Profile
    {
        public MythMapperProfile()
        {
            CreateMap<Myth, MythModel>().ReverseMap();

            CreateMap<Myth, MythModel>()
             .ForMember(dest => dest.DeityName, opt => opt.MapFrom(src => src.Deity.Name));

            CreateMap<MythCreateModel, Myth>().ReverseMap();
            CreateMap<MythUpdateModel, Myth>().ReverseMap();
        }
    }
}
