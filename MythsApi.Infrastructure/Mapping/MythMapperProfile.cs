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
        }
    }
}
